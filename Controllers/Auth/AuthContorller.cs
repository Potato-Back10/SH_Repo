using Gamza.Dtos;
using Gamza.Models;
using Gamza.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [SwaggerOperation(
        Summary = "로그인",
        Description = "아이디와 비밀번호를 이용하여 로그인합니다.",
        Tags = new[] { "로그인 및 회원가입" }
    )]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequestDto dto,
        CancellationToken ct = default
    )
    {
        var token = await _authService.LoginAsync(dto.LoginID, dto.Password, ct);
        if (token is null)
            return Unauthorized("아이디 또는 비밀번호가 올바르지 않습니다.");

        return Ok(new { Token = token });
    }
}
