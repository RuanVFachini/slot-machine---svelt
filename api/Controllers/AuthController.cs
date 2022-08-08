using Api.Logins;
using Api.Tokens;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("auth")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly ITokenService _tokenService;

    public UserController(ILogger<UserController> logger, ITokenService tokenService)
    {
        _logger = logger;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public ActionResult<string> Login([FromBody] LoginRequest request)
    {
        return Ok(_tokenService.Create(request.Username));
    }
}
