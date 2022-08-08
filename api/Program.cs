using System.Net;
using System.Net.WebSockets;
using Api.Extensions;
using Api.Levers;
using Api.Players;
using Api.Tokens;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ISessionService>(new SessionService());
builder.Services.AddScoped<ILeverService, LeverService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                        // policy.AllowAnyOrigin();
                        policy.SetIsOriginAllowed(origin => true);
                        policy.AllowAnyHeader();
                        policy.AllowAnyMethod();
                        policy.AllowCredentials();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var webSocketOptions = new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromMinutes(2),
};

app.UseWebSockets(webSocketOptions);

app.Map("score", async (
  HttpContext context,
  [FromServices] ILeverService leverService,
  [FromServices] ITokenService tokenService,
  [FromServices] ISessionService sessionService) => {

  context.Request.Query.TryGetValue("Token", out var token);

  if (!context.WebSockets.IsWebSocketRequest || token == string.Empty) {
    context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
    return;
  }

  var jwtValidation = tokenService.Validate(token);

  if (jwtValidation.Authenticated) {
    using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
  
    while(true) {
      await webSocket.SendAsync(sessionService.Sessions);
      Thread.Sleep(1000);
    }
  }
  
});

app.Map("sort", async (
  HttpContext context,
  [FromServices] ILeverService leverService,
  [FromServices] ITokenService tokenService) => {
  
  context.Request.Query.TryGetValue("Token", out var token);

  if (!context.WebSockets.IsWebSocketRequest || token == string.Empty) {
    context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
    return;
  }
  
  var jwtValidation = tokenService.Validate(token);

  if (jwtValidation.Authenticated) {
    using var webSocket = await context.WebSockets.AcceptWebSocketAsync();

    await leverService.LeverAsync(webSocket, jwtValidation.Username);
  }
});

await app.RunAsync();
