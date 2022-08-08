using System.Net;
using System.Net.WebSockets;
using Api.Extensions;
using Api.Levers;
using Api.Tokens;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ILeverService>(new LeverService());
builder.Services.AddSingleton<ITokenService>(new TokenService());

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
  [FromServices] ITokenService tokenService) => {

  if (!context.WebSockets.IsWebSocketRequest) {
    context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
    return;
  }

  var token = context.Request.Query["Token"];
  var jwtValidation = tokenService.Validate(token);

  if (jwtValidation.Authenticated) {
    using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
  
    while(true) {
      await webSocket.SendAsync(leverService.ScoreList);
      Thread.Sleep(1000);
    }
  }
  
});

app.Map("sort", async (
  HttpContext context,
  [FromServices] ILeverService leverService,
  [FromServices] ITokenService tokenService) => {

  if (!context.WebSockets.IsWebSocketRequest) {
    context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
    return;
  }

  var token = context.Request.Query["Token"];
  var jwtValidation = tokenService.Validate(token);

  if (jwtValidation.Authenticated) {
    using var webSocket = await context.WebSockets.AcceptWebSocketAsync();

    await leverService.LeverAsync(webSocket, jwtValidation.Username);
  }
});

await app.RunAsync();
