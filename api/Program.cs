using System.Net;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
using Api.Extensions;
using Api.Levers;
using Api.Players;
using Api.Tokens;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var websocketPaths = new string[] {"/sort", "/score"};

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

app.Use(async (context, next) =>
{
  if (websocketPaths.Contains(context.Request.Path.Value))
  {
    var token = context.Request.Query["Token"];
    if (!string.IsNullOrWhiteSpace(token))
    {
        var tokenService = context.RequestServices.GetRequiredService<ITokenService>();
        var principal = tokenService.Validate(token);

        if (principal != null) {
          context.User = principal;
          await next(context);
        } else {
          context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
          context.Response.Body.Write(Encoding.ASCII.GetBytes("Invalid Token"));
        }

    } else {
      context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
      context.Response.Body.Write(Encoding.ASCII.GetBytes("Token is Required"));
    }
  } else {
    await next(context);
  }
});

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

  if (!context.WebSockets.IsWebSocketRequest) {
    context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
    return;
  }

  using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
  
  while(true) {
    await webSocket.SendAsync(sessionService.Sessions);
    Thread.Sleep(1000);
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

  using var webSocket = await context.WebSockets.AcceptWebSocketAsync();

  await leverService.LeverAsync(webSocket, context.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
});

await app.RunAsync();
