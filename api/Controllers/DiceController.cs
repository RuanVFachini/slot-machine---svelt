using Api.Dices;
using Api.Players;
using Api.Scores;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DiceController : ControllerBase
{
    private readonly ILogger<DiceController> _logger;
    private readonly LeverService _leverService;

    public DiceController(ILogger<DiceController> logger, LeverService leverService)
    {
        _logger = logger;
        _leverService = leverService;
    }

    [HttpPost("sort")]
    public ActionResult<DiceResult> Sort([FromBody] DiceRequest request)
    {
        var sortResult = _leverService.Sort(request.Sides);

        return Ok(sortResult);
    }
}
