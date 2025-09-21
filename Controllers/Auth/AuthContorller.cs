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
    private readonly UserService _userService;
    private readonly JwtService _jwtService;

    public AuthController(UserService userService, JwtService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
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
        try
        {
            var user = await _userService.ValidateUserAsync(dto.LoginID, dto.Password, ct);
            if (user is null)
                return Unauthorized(new { message = "아이디 또는 비밀번호가 올바르지 않습니다." });

            var token = _jwtService.GenerateToken(user.Id, "User");

            return Ok(
                new
                {
                    token,
                    LoginID = user.LoginID,
                    Username = user.Username,
                }
            );
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"서버 오류: {ex.Message}" });
        }
    }
}
