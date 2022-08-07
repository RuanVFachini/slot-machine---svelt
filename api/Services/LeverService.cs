using System.Net.WebSockets;
using Api.Dices;
using Api.Extensions;
using Api.Players;
using Api.Scores;

namespace Api.Services;
public class LeverService {

    public readonly ScoreList ScoreList;

    public LeverService()
    {
        ScoreList = new ScoreList();
    }

    public async Task LeverAsync(WebSocket webSocket)
    {
        var message = await webSocket.ReceiveAsync<DiceRequest>();

        while (!message.ReceiveResult.CloseStatus.HasValue)
        {
            if (message.Message != null) {
                await webSocket.SendAsync(Sort(message.Message.Sides));
            }

            await webSocket.ReceiveAsync<DiceRequest>();
        }

        await webSocket.CloseAsync(
            message.ReceiveResult.CloseStatus.Value,
            message.ReceiveResult.CloseStatusDescription,
            CancellationToken.None);
    }

    private int DiceSort(int razon){
        return new Random().Next(5, 10) * razon;
    }

    public DiceResult Sort(int sides)
    {
        var razon = 360 / sides;

        var dice1Steps = DiceSort(razon);
        var dice2Steps = DiceSort(razon);
        var dice3Steps = DiceSort(razon);

        var sortResult = new DiceResult(dice1Steps, dice2Steps, dice3Steps);

        if (sortResult.Winner) {
            var player = ScoreList.FirstOrDefault(x => x.Name == "player");

            if (player == null) {
                player = new Player("player", 0);
                ScoreList.Add(player);
            }

            player.Score++;
        }

        return sortResult;
    }
}
