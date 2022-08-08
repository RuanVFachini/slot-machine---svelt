using Api.Logins;
using Api.Players;
using Api.Tokens;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("auth")]
public class UserController : ControllerBase
{
    private readonly ISessionService _sessionService;

    private readonly ILogger<UserController> _logger;
    private readonly ITokenService _tokenService;

    public UserController(
        ILogger<UserController> logger,
        ITokenService tokenService,
        ISessionService sessionService)
    {
        _logger = logger;
        _tokenService = tokenService;
        _sessionService = sessionService;
    }

    [HttpPost("login")]
    public ActionResult<string> Login([FromBody] LoginRequest request)
    {
        var result = _sessionService.CreateSession(request.Username);

        return result
            ? Ok(_tokenService.Create(request.Username))
            : BadRequest("Username already registered");

    }
}
