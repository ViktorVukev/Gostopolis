using Gostopolis.Infrastructure;
using Gostopolis.Services;
using Gostopolis.Statistics.Data;
using Gostopolis.Statistics.Messages;
using Gostopolis.Statistics.Statistics;
using static Gostopolis.Constants;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddWebService<StatisticsDbContext>(builder.Configuration, PartnersDescription)
    .AddTransient<IDataSeeder, StatisticsDataSeeder>()
    .AddTransient<IStatisticsService, StatisticsService>()
    .AddMessaging(
        builder.Configuration,
        typeof(RestaurantCreatedConsumer),
        typeof(AccommodationCreatedConsumer),
        typeof(PartnerApprovedConsumer),
        typeof(ReservationCreatedConsumer));

var app = builder.Build();

app
    .UseWebService(builder.Environment)
    .Initialize();

app.Run();