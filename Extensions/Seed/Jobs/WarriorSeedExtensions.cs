using Gamza.Enums;
using Gamza.Models;
using Microsoft.EntityFrameworkCore;

namespace Gamza.Extensions.Seed.Jobs
{
    internal static class WarriorSeedExtensions
    {
        public static void SeedWarriorBranch(this ModelBuilder mb)
        {
            mb.Entity<Job>()
                .HasData(
                    // 1차
                    new Job
                    {
                        Id = JobSeedIds.Warrior,
                        Code = "warrior",
                        Name = "워리어",
                        Tier = JobTier.First,
                        MinLevel = 10,
                    },
                    // 2차
                    new Job
                    {
                        Id = JobSeedIds.Knight,
                        Code = "knight",
                        Name = "나이트",
                        Tier = JobTier.Second,
                        MinLevel = 30,
                        ParentId = JobSeedIds.Warrior,
                    },
                    new Job
                    {
                        Id = JobSeedIds.Spearman,
                        Code = "spearman",
                        Name = "스피어맨",
                        Tier = JobTier.Second,
                        MinLevel = 30,
                        ParentId = JobSeedIds.Warrior,
                    },
                    new Job
                    {
                        Id = JobSeedIds.Ronin,
                        Code = "ronin",
                        Name = "로닌",
                        Tier = JobTier.Second,
                        MinLevel = 30,
                        ParentId = JobSeedIds.Warrior,
                    },
                    // 3차
                    new Job
                    {
                        Id = JobSeedIds.Paladin,
                        Code = "paladin",
                        Name = "팔라딘",
                        Tier = JobTier.Third,
                        MinLevel = 70,
                        ParentId = JobSeedIds.Knight,
                    },
                    new Job
                    {
                        Id = JobSeedIds.DragonKnight,
                        Code = "dragon_knight",
                        Name = "드래곤 나이트",
                        Tier = JobTier.Third,
                        MinLevel = 70,
                        ParentId = JobSeedIds.Spearman,
                    },
                    new Job
                    {
                        Id = JobSeedIds.Blade,
                        Code = "blade",
                        Name = "블레이드",
                        Tier = JobTier.Third,
                        MinLevel = 70,
                        ParentId = JobSeedIds.Ronin,
                    }
                );
        }
    }
}
