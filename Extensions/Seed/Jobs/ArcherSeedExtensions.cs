using Gamza.Enums;
using Gamza.Models;
using Microsoft.EntityFrameworkCore;

namespace Gamza.Extensions.Seed.Jobs
{
    internal static class ArcherSeedExtensions
    {
        public static void SeedArcherBranch(this ModelBuilder mb)
        {
            mb.Entity<Job>()
                .HasData(
                    // 1차
                    new Job
                    {
                        Id = JobSeedIds.Archer,
                        Code = "archer",
                        Name = "궁수",
                        Tier = JobTier.First,
                        MinLevel = 10,
                    },
                    // 2차
                    new Job
                    {
                        Id = JobSeedIds.Hunter,
                        Code = "hunter",
                        Name = "헌터",
                        Tier = JobTier.Second,
                        MinLevel = 30,
                        ParentId = JobSeedIds.Archer,
                    },
                    new Job
                    {
                        Id = JobSeedIds.Sniper,
                        Code = "sniper",
                        Name = "명사수",
                        Tier = JobTier.Second,
                        MinLevel = 30,
                        ParentId = JobSeedIds.Archer,
                    },
                    new Job
                    {
                        Id = JobSeedIds.TrickArcher,
                        Code = "TrickArcher",
                        Name = "트릭아처",
                        Tier = JobTier.Second,
                        MinLevel = 30,
                        ParentId = JobSeedIds.Archer,
                    },
                    // 3차
                    new Job
                    {
                        Id = JobSeedIds.BeastMaster,
                        Code = "beastmaster",
                        Name = "비스트마스터",
                        Tier = JobTier.Third,
                        MinLevel = 70,
                        ParentId = JobSeedIds.Hunter,
                    },
                    new Job
                    {
                        Id = JobSeedIds.DeadEye,
                        Code = "deadeye",
                        Name = "데드아이",
                        Tier = JobTier.Third,
                        MinLevel = 70,
                        ParentId = JobSeedIds.Sniper,
                    },
                    new Job
                    {
                        Id = JobSeedIds.Mirage,
                        Code = "mirage",
                        Name = "미라지",
                        Tier = JobTier.Third,
                        MinLevel = 70,
                        ParentId = JobSeedIds.TrickArcher,
                    }
                );
        }
    }
}
