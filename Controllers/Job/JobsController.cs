using Gamza.Data;
using Gamza.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gamza.Controllers
{
    [ApiController]
    [Route("api/jobs")]
    public class JobsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public JobsController(AppDbContext db)
        {
            _db = db;
        }

        //전직 트리(직업 마스터) 단순 조회
        [HttpGet("tree")]
        public async Task<IActionResult> GetTree(CancellationToken ct)
        {
            var jobs = await _db
                .Jobs.AsNoTracking()
                .OrderBy(j => j.Tier)
                .ThenBy(j => j.Id)
                .Select(j => new
                {
                    j.Id,
                    j.Code,
                    j.Name,
                    j.Tier,
                    j.MinLevel,
                    j.ParentId,
                })
                .ToListAsync(ct);

            return Ok(jobs);
        }
    }

    //플레이어 전직(가능 목록 / 전직 수행) API

    [ApiController]
    [Route("v1/players/{playerId:int}/jobs")]
    public class PlayerJobController : ControllerBase
    {
        private readonly IJobService _jobService;

        public PlayerJobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        //현재 전직 가능한 직업 목록
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailable(int playerId, CancellationToken ct)
        {
            var list = await _jobService.GetAvailableAdvancementsAsync(playerId, ct);
            var dto = list.Select(j => new
            {
                j.Id,
                j.Code,
                j.Name,
                j.Tier,
                j.MinLevel,
            });
            return Ok(dto);
        }

        public record AdvanceRequest(int TargetJobId);

        //전직 수행 (targetJobId는 현재 직업의 자식이어야 함)
        [HttpPost("advance")]
        public async Task<IActionResult> Advance(
            int playerId,
            [FromBody] AdvanceRequest body,
            CancellationToken ct
        )
        {
            try
            {
                await _jobService.AdvanceAsync(playerId, body.TargetJobId, ct);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
