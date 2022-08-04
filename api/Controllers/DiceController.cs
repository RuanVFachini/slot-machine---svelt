using Api.Dice;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DiceController : ControllerBase
{
    private readonly ILogger<DiceController> _logger;

    public DiceController(ILogger<DiceController> logger)
    {
        _logger = logger;
    }

    [HttpPost("sort")]
    public ActionResult<DiceResult> Sort([FromBody] DiceRequest request)
    {
        var razon = 360 / request.Sides;

        var dice1Steps = new Random().Next(5, 20) * razon;
        var dice2Steps = new Random().Next(5, 20) * razon;
        var dice3Steps = new Random().Next(5, 20) * razon;

        return Ok(new DiceResult(dice1Steps, dice2Steps, dice3Steps));
    }
}
