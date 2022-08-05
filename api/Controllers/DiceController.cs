using Api.Dices;
using Api.Players;
using Api.Scores;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DiceController : ControllerBase
{
    private readonly ILogger<DiceController> _logger;
    private readonly ScoreList _scoreList;

    public DiceController(ILogger<DiceController> logger, ScoreList scoreList)
    {
        _logger = logger;
        _scoreList = scoreList;

    }

    private int DiceSort(int razon){
        return new Random().Next(5, 10) * razon;
    }

    [HttpPost("sort")]
    public ActionResult<DiceResult> Sort([FromBody] DiceRequest request)
    {
        var razon = 360 / request.Sides;

        var dice1Steps = DiceSort(razon);
        var dice2Steps = DiceSort(razon);
        var dice3Steps = DiceSort(razon);

        var sortResult = new DiceResult(dice1Steps, dice2Steps, dice3Steps);

        if (sortResult.Winner) {
            var player = _scoreList.FirstOrDefault(x => x.Name == "player");

            if (player == null) {
                player = new Player("player", 0);
                _scoreList.Add(player);
            }

            player.Score++;
        }

        return Ok(sortResult);
    }
}
