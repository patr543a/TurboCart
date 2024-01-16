using TurboCart.Infrastructure.Networking.Grpc.Services;
using TurboCart.Infrastructure.Networking.Services;
using TurboCart.Infrastructure.Networking.SignalR.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddSingleton<ITurboCartSessionService, TurboCartSessionService>();
builder.Services.AddSingleton<SessionService, SessionService>();

builder.Services.AddGrpc();
builder.Services.AddSignalR();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
app.MapHub<SessionHub>("/Session");
app.MapGrpcService<SessionService>();

app.Run();
