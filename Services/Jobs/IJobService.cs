using Gamza.Models;

namespace Gamza.Services
{
    public interface IJobService
    {
        /// 현재 플레이어가 레벨 조건을 만족해 전직 가능한 목록(현재 직업의 Children 중 MinLevel 충족)을 반환
        Task<IReadOnlyList<Job>> GetAvailableAdvancementsAsync(int playerId, CancellationToken ct);

        // 전직 실행(부모-자식 경로 유효성 + 레벨 컷 검증 포함)
        Task AdvanceAsync(int playerId, int targetJobId, CancellationToken ct);
    }
}
