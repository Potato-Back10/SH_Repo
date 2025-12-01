using System.Security.Claims;
using Gamza.Dtos;
using Gamza.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Gamza.Controllers;

[ApiController]
[Route("api/players")]
[Authorize] // 로그인 필수
public class PlayerController : ControllerBase
{
    private readonly PlayerService _playerService;

    public PlayerController(PlayerService playerService)
    {
        _playerService = playerService;
    }
    private int GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                          ?? throw new Exception("토큰에 사용자 ID가 없습니다.");
        return int.Parse(userIdClaim);
    }

    // 내 캐릭터 목록
    [HttpGet("me")]
    [SwaggerOperation(Summary = "내 계정의 캐릭터 목록 조회")]
    public async Task<ActionResult<List<PlayerResponseDto>>> GetMyPlayers(
        CancellationToken ct = default)
    {
        var userId = GetUserId();
        var players = await _playerService.GetMyPlayersAsync(userId, ct);

        var result = players.Select(p => new PlayerResponseDto
        {
            Id = p.Id,
            NickName = p.NickName,
            Level = p.Level,
            Exp = p.Exp,
            Job = p.Job,
            PlayerStauts = p.PlayerStauts
        }).ToList();

        return Ok(result);
    }

    // 캐릭터 생성
    [HttpPost]
    [SwaggerOperation(Summary = "새 캐릭터 생성")]
    public async Task<ActionResult<PlayerResponseDto>> CreatePlayer(
        [FromBody] PlayerCreateDto dto,
        CancellationToken ct = default)
    {
        var userId = GetUserId();
        var player = await _playerService.CreatePlayerAsync(userId, dto, ct);

        var result = new PlayerResponseDto
        {
            Id = player.Id,
            NickName = player.NickName,
            Level = player.Level,
            Exp = player.Exp,
            Job = player.Job,
            PlayerStauts = player.PlayerStauts
        };

        return CreatedAtAction(nameof(GetMyPlayers), new { id = result.Id }, result);
    }

    // 캐릭터 삭제
    [HttpDelete("{id:int}")]
    [SwaggerOperation(Summary = "내 캐릭터 삭제")]
    public async Task<IActionResult> DeletePlayer(
        int id,
        CancellationToken ct = default)
    {
        var userId = GetUserId();
        await _playerService.DeleteMyPlayerAsync(userId, id, ct);
        return NoContent();
    }
}
