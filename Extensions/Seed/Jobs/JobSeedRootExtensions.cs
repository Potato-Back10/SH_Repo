using Microsoft.EntityFrameworkCore;

namespace Gamza.Extensions.Seed.Jobs
{
    public static class JobSeedRootExtensions
    {
        public static void SeedAllJobs(this ModelBuilder mb)
        {
            // 공통 엔티티 구성은 기존에 한 번만 호출(이미 있다면 생략)
            mb.ConfigureJobEntity();

            // 가지별 호출
            mb.SeedWarriorBranch();
            mb.SeedThiefBranch();
            mb.SeedMageBranch();
            mb.SeedArcherBranch();
        }
    }
}
