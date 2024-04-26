using System.Reflection;
using Gostopolis.Admin.Infrastructure;
using Gostopolis.Admin.Services;
using Gostopolis.Admin.Services.Accommodations;
using Gostopolis.Admin.Services.Identity;
using Gostopolis.Admin.Services.Restaurants;
using Gostopolis.Infrastructure;
using Gostopolis.Services.Identity;
using Microsoft.AspNetCore.Mvc;
using Refit;

var builder = WebApplication.CreateBuilder(args);

var serviceEndpoints = builder.Configuration
    .GetSection(nameof(ServiceEndpoints))
    .Get<ServiceEndpoints>(config => config.BindNonPublicProperties = true);

builder.Services
    .AddAutoMapperProfile(Assembly.GetExecutingAssembly())
    .AddTokenAuthentication(builder.Configuration)
    .AddScoped<ICurrentTokenService, CurrentTokenService>()
    .AddTransient<JwtCookieAuthenticationMiddleware>()
    .AddControllersWithViews(options => options
        .Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));

builder.Services
    .AddRefitClient<IIdentityService>()
    .WithConfiguration(serviceEndpoints.Identity);

builder.Services
    .AddRefitClient<IAccommodationService>()
    .WithConfiguration(serviceEndpoints.Accommodations);

builder.Services
    .AddRefitClient<IRestaurantService>()
    .WithConfiguration(serviceEndpoints.Restaurants);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app
        .UseDeveloperExceptionPage();
}
else
{
    app
        .UseExceptionHandler("/Home/Error")
        .UseHsts();
}

app
    .UseHttpsRedirection()
    .UseStaticFiles()
    .UseRouting()
    .UseJwtCookieAuthentication()
    .UseAuthorization()
    .UseEndpoints(endpoints => endpoints
        .MapDefaultControllerRoute());

app.Run();
