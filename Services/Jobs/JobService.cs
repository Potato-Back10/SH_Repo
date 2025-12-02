using Gamza.Data;
using Gamza.Models;
using Microsoft.EntityFrameworkCore;

namespace Gamza.Services
{
    public class JobService : IJobService
    {
        private readonly AppDbContext _db;

        public JobService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IReadOnlyList<Job>> GetAvailableAdvancementsAsync(
            int playerId,
            CancellationToken ct
        )
        {
            var player =
                await _db
                    .Players.Include(p => p.CurrentJob)
                    .ThenInclude(j => j.Children)
                    .SingleOrDefaultAsync(p => p.Id == playerId, ct)
                ?? throw new KeyNotFoundException("Player not found");

            var list = player
                .CurrentJob.Children.Where(c => player.Level >= c.MinLevel)
                .OrderBy(c => c.Tier)
                .ThenBy(c => c.Id)
                .ToList();

            return list;
        }

        public async Task AdvanceAsync(int playerId, int targetJobId, CancellationToken ct)
        {
            var player =
                await _db
                    .Players.Include(p => p.CurrentJob)
                    .SingleOrDefaultAsync(p => p.Id == playerId, ct)
                ?? throw new KeyNotFoundException("Player not found");

            var target =
                await _db.Jobs.AsNoTracking().SingleOrDefaultAsync(j => j.Id == targetJobId, ct)
                ?? throw new KeyNotFoundException("Target job not found");

            if (target.ParentId != player.CurrentJobId)
                throw new InvalidOperationException("해당 전직 경로로는 전직할 수 없습니다.");

            if (player.Level < target.MinLevel)
                throw new InvalidOperationException(
                    $"레벨이 부족합니다. 필요 레벨: {target.MinLevel}"
                );

            player.CurrentJobId = target.Id;

            _db.PlayerJobHistories.Add(
                new PlayerJobHistory
                {
                    PlayerId = player.Id,
                    JobId = target.Id,
                    ChangedAtUtc = DateTime.UtcNow,
                }
            );

            await _db.SaveChangesAsync(ct);
        }
    }
}
