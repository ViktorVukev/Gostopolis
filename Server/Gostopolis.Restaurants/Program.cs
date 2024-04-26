using Gostopolis.Infrastructure;
using Gostopolis.Restaurants.Data;
using Gostopolis.Restaurants.Services;
using Gostopolis.Restaurants.Services.Identity;
using Gostopolis.Restaurants.Services.Images;
using Gostopolis.Restaurants.Services.Ingredients;
using Gostopolis.Restaurants.Services.Menus;
using Gostopolis.Restaurants.Services.Products;
using Gostopolis.Restaurants.Services.Reservations;
using Gostopolis.Restaurants.Services.Restaurants;
using Gostopolis.Restaurants.Services.RestaurantType;
using Gostopolis.Restaurants.Services.Schedule;
using Gostopolis.Restaurants.Services.Tables;
using Gostopolis.Restaurants.Services.WorkingTimes;
using Refit;
using static Gostopolis.Constants;

var builder = WebApplication.CreateBuilder(args);

var serviceEndpoints = builder.Configuration
    .GetSection(nameof(ServiceEndpoints))
    .Get<ServiceEndpoints>(config => config.BindNonPublicProperties = true);

builder.Services
    .AddWebService<RestaurantsDbContext>(builder.Configuration, PartnersDescription)
    .AddTransient<IRestaurantTypeService, RestaurantTypeService>()
    .AddTransient<IImageService, ImageService>()
    .AddTransient<IIngredientService, IngredientService>()
    .AddTransient<IReservationService, ReservationService>()
    .AddTransient<ITableService, TableService>()
    .AddTransient<IMenuService, MenuService>()
    .AddTransient<IProductService, ProductService>()
    .AddTransient<IWorkingTimeService, WorkingTimeService>()
    .AddTransient<IRestaurantService, RestaurantService>()
    .AddMessaging(builder.Configuration);

builder.Services
    .AddRefitClient<IScheduleService>()
    .WithConfiguration(serviceEndpoints.Schedule);

builder.Services
    .AddRefitClient<IIdentityService>()
    .WithConfiguration(serviceEndpoints.Identity);

var app = builder.Build();

app
    .UseWebService(builder.Environment)
    .Initialize();

app.Run();