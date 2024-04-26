using Gostopolis.Infrastructure;
using Gostopolis.Schedule.Data;
using Gostopolis.Schedule.Messages;
using Gostopolis.Schedule.Services;
using static Gostopolis.Constants;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddWebService<ScheduleDbContext>(builder.Configuration, PartnersDescription)
    .AddTransient<IRoomScheduleService, RoomScheduleService>()
    .AddTransient<ITableScheduleService, TableScheduleService>()
    .AddMessaging(
        builder.Configuration,
        typeof(RoomOccupiedConsumer),
        typeof(TableOccupiedConsumer));

var app = builder.Build();

app
    .UseWebService(builder.Environment)
    .Initialize();

app.Run();