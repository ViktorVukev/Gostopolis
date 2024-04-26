using Gostopolis.Identity.Data;
using Gostopolis.Identity.Infrastructure;
using Gostopolis.Identity.Services.Applications;
using Gostopolis.Identity.Services.Identity;
using Gostopolis.Identity.Services.Users;
using Gostopolis.Infrastructure;
using Gostopolis.Services;
using static Gostopolis.Constants;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddWebService<IdentityDbContext>(builder.Configuration, IdentityDescription)
    .AddUserStorage()
    .AddTransient<IDataSeeder, IdentityDataSeeder>()
    .AddTransient<ITokenGeneratorService, TokenGeneratorService>()
    .AddTransient<IIdentityService, IdentityService>()
    .AddTransient<IUserService, UserService>()
    .AddTransient<IApplicationService, ApplicationService>()
    .AddMessaging(builder.Configuration);


var app = builder.Build();

app
    .UseWebService(builder.Environment)
    .Initialize();

app.Run();
