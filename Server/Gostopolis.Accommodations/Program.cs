using System.Reflection;
using Gostopolis.Accommodations.Data;
using Gostopolis.Accommodations.Services;
using Gostopolis.Accommodations.Services.Accommodations;
using Gostopolis.Accommodations.Services.AccommodationType;
using Gostopolis.Accommodations.Services.Images;
using Gostopolis.Accommodations.Services.Meal;
using Gostopolis.Accommodations.Services.Reservations;
using Gostopolis.Accommodations.Services.Rooms;
using Gostopolis.Accommodations.Services.Schedule;
using Gostopolis.Infrastructure;
using Refit;
using static Gostopolis.Constants;

var builder = WebApplication.CreateBuilder(args);

var serviceEndpoints = builder.Configuration
    .GetSection(nameof(ServiceEndpoints))
    .Get<ServiceEndpoints>(config => config.BindNonPublicProperties = true);

builder.Services
    .AddWebService<AccommodationsDbContext>(builder.Configuration, PartnersDescription)
    .AddTransient<IAccommodationService, AccommodationService>()
    .AddTransient<IRoomService, RoomService>()
    .AddTransient<IMealService, MealService>()
    .AddTransient<IReservationService, ReservationService>()
    .AddTransient<IImageService, ImageService>()
    .AddTransient<IAccommodationTypeService, AccommodationTypeService>()
    .AddMessaging(builder.Configuration);

builder.Services
    .AddRefitClient<IScheduleService>()
    .WithConfiguration(serviceEndpoints.Schedule);

var app = builder.Build();

app
    .UseWebService(builder.Environment)
    .Initialize();

app.Run();