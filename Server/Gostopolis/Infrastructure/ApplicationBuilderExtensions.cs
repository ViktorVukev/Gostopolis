namespace Gostopolis.Infrastructure;

using Hangfire;
using HealthChecks.UI.Client;
using Messages;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseWebService(
        this IApplicationBuilder app,
        IWebHostEnvironment env)
    {
        app
            .UseCors(options => options
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod())
            .UseHttpsRedirection()
            .UseRouting()
            .UseAuthentication()
            .UseAuthorization()
            .UseSwaggerApi()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

                endpoints.MapControllers();
            });

        return app;
    }

    /// <summary>
    /// Migrates and seeds db contexts of the current application.
    /// </summary>
    /// <param name="app">The current builder</param>
    /// <returns></returns>
    public static IApplicationBuilder Initialize(
        this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var serviceProvider = serviceScope.ServiceProvider;

        var db = serviceProvider.GetRequiredService<DbContext>();

        if (app.ApplicationServices.GetService<MessagesHostedService>() != null)
        {
            app.UseHangfireDashboard();
        }

        db.Database.Migrate();

        var seeders = serviceProvider.GetServices<IDataSeeder>();

        foreach (var seeder in seeders)
        {
            seeder.SeedData();
        }

        return app;
    }

    private static IApplicationBuilder UseSwaggerApi(
        this IApplicationBuilder app)
    {
        app.UseSwagger()
            .UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });

        return app;
    }
}