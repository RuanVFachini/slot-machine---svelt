using System.Net.WebSockets;
using Api.Dices;
using Api.Extensions;
using Api.Players;

namespace Api.Levers;

public interface ILeverService
{
    Task LeverAsync(WebSocket webSocket, string username);
    DiceResult Sort(int sides, string username);
}

public class LeverService: ILeverService
{
    private readonly ISessionService _sessionService;

    public LeverService(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    public async Task LeverAsync(WebSocket webSocket, string username)
    {
        var message = await webSocket.ReceiveAsync<DiceRequest>();

        while (!message.ReceiveResult.CloseStatus.HasValue)
        {
            if (message.Message != null) {
                var sortResult = Sort(message.Message.Sides, username);

                if (sortResult.Winner) {
                    _sessionService.IncreaseScore(username);
                }
                
                await webSocket.SendAsync(sortResult);
            }

            message = await webSocket.ReceiveAsync<DiceRequest>();
        }

        _sessionService.DeleteSession(username);

        await webSocket.CloseAsync(
            message.ReceiveResult.CloseStatus.Value,
            message.ReceiveResult.CloseStatusDescription,
            CancellationToken.None);
    }

    private int DiceSort(int razon){
        return new Random().Next(5, 10) * razon;
    }

    public DiceResult Sort(int sides, string username)
    {
        var razon = 360 / sides;

        var dice1Steps = DiceSort(razon);
        var dice2Steps = DiceSort(razon);
        var dice3Steps = DiceSort(razon);

        return new DiceResult(dice1Steps, dice2Steps, dice3Steps);
    }
}
