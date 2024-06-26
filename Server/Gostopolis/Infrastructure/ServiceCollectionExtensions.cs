﻿namespace Gostopolis.Infrastructure;

using System.Reflection;
using System.Text;
using Hangfire;
using MassTransit;
using Messages;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models;
using Services.Emails;
using Services.Files;
using Services.Identity;
using Services.Properties;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebService<TDbContext>(
        this IServiceCollection services,
        IConfiguration configuration,
        string projectDescription)
        where TDbContext : DbContext
    {
        services
            .AddDatabase<TDbContext>(configuration)
            .AddApplicationSettings(configuration)
            .AddTokenAuthentication(configuration)
            .AddSwagger(Assembly.GetCallingAssembly(), projectDescription)
            .AddAutoMapperProfile(Assembly.GetCallingAssembly())
            .AddHealth(configuration)
            .AddControllers();

        return services;
    }

    public static IServiceCollection AddDatabase<TDbContext>(
        this IServiceCollection services,
        IConfiguration configuration)
        where TDbContext : DbContext
        => services
            .AddScoped<DbContext, TDbContext>()
            .AddDbContext<TDbContext>(options => options
                .UseSqlServer(
                    configuration.GetDefaultConnectionString(),
                    sqlOptions => sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null)));

    public static IServiceCollection AddApplicationSettings(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .Configure<ApplicationSettings>(
                configuration.GetSection(nameof(ApplicationSettings)),
                config => config.BindNonPublicProperties = true);

    public static IServiceCollection AddTokenAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var secret = configuration
            .GetSection(nameof(ApplicationSettings))
            .GetValue<string>(nameof(ApplicationSettings.Secret));

        var key = Encoding.ASCII.GetBytes(secret);

        services
            .AddAuthentication(authentication =>
            {
                authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(bearer =>
            {
                bearer.RequireHttpsMetadata = false;
                bearer.SaveToken = true;
                bearer.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IPropertyService, PropertyService>();

        return services;
    }

    public static IServiceCollection AddSwagger(
        this IServiceCollection services,
        Assembly assembly,
        string description)
        => services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = assembly.GetName().ToString().Split(", ")[0] + " API",
                Description = description,
            });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });

            var xmlFilename = $"{assembly.GetName().Name?.Split(", ")[0]}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

    public static IServiceCollection AddAutoMapperProfile(
        this IServiceCollection services,
        Assembly assembly)
        => services
            .AddAutoMapper(
                (_, config) => config
                    .AddProfile(new MappingProfile(assembly)),
                Array.Empty<Assembly>());

    public static IServiceCollection AddHealth(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var healthChecks = services.AddHealthChecks();

        healthChecks.AddSqlServer(configuration.GetDefaultConnectionString());

        return services;
    }

    public static IServiceCollection AddMessaging(
        this IServiceCollection services,
        IConfiguration configuration,
        params Type[] consumers)
    {
        services
            .AddMassTransit(mt =>
            {
                consumers.ForEach(consumer => mt.AddConsumer(consumer));

                mt.UsingAzureServiceBus((context, cfg) =>
                {
                    cfg.Host("Endpoint=sb://gostopolis.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=mV1gohkbW0AVo8yjcfFPy2D8caMUdZ/QE+ASbNba1xE=");

                    consumers.ForEach(consumer =>
                    {
                        cfg.ReceiveEndpoint(consumer.FullName, endpoint =>
                        {
                            endpoint.ConfigureConsumer(context, consumer);
                        });
                    });
                });
            });

        services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(configuration.GetDefaultConnectionString()));

        services.AddHangfireServer();

        return services;
    }
}