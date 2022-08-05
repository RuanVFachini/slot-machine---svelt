using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Api.Dices;
using Api.Scores;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(new ScoreList());

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
    KeepAliveInterval = TimeSpan.FromMinutes(2)
};

app.UseWebSockets(webSocketOptions);

app.Map("score", async (HttpContext context, [FromServices] ScoreList list) => {
  if (!context.WebSockets.IsWebSocketRequest) {
    context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
  }
  using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
  
  while(true) {
    var data = Encoding.ASCII.GetBytes(JsonSerializer.Serialize(list, new JsonSerializerOptions() {
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    }));
    await webSocket.SendAsync(data, WebSocketMessageType.Text, true, CancellationToken.None);
    Thread.Sleep(1000);
  }
});

await app.RunAsync();
