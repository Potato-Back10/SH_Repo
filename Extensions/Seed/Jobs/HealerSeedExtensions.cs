using Gamza.Enums;
using Gamza.Models;
using Microsoft.EntityFrameworkCore;

namespace Gamza.Extensions.Seed.Jobs
{
    internal static class HealerSeedExtensions
    {
        public static void SeedArcherBranch(this ModelBuilder mb)
        {
            mb.Entity<Job>()
                .HasData(
                    // 1차
                    new Job
                    {
                        Id = JobSeedIds.Healer,
                        Code = "healer",
                        Name = "힐러",
                        Tier = JobTier.First,
                        MinLevel = 10,
                    },
                    // 2차
                    new Job
                    {
                        Id = JobSeedIds.Cleric,
                        Code = "cleric",
                        Name = "클레릭",
                        Tier = JobTier.Second,
                        MinLevel = 30,
                        ParentId = JobSeedIds.Healer,
                    },
                    new Job
                    {
                        Id = JobSeedIds.Onmyoji,
                        Code = "onmyoji",
                        Name = "음양사",
                        Tier = JobTier.Second,
                        MinLevel = 30,
                        ParentId = JobSeedIds.Healer,
                    },
                    new Job
                    {
                        Id = JobSeedIds.Alchemist,
                        Code = "alchemist",
                        Name = "연금술사",
                        Tier = JobTier.Second,
                        MinLevel = 30,
                        ParentId = JobSeedIds.Healer,
                    },
                    // 3차
                    new Job
                    {
                        Id = JobSeedIds.Priest,
                        Code = "priest",
                        Name = "프리스트",
                        Tier = JobTier.Third,
                        MinLevel = 70,
                        ParentId = JobSeedIds.Cleric,
                    },
                    new Job
                    {
                        Id = JobSeedIds.Sorcerer,
                        Code = "sorcerer",
                        Name = "점술사",
                        Tier = JobTier.Third,
                        MinLevel = 70,
                        ParentId = JobSeedIds.Onmyoji,
                    },
                    new Job
                    {
                        Id = JobSeedIds.Homunculus,
                        Code = "homunculus",
                        Name = "호문쿨루스",
                        Tier = JobTier.Third,
                        MinLevel = 70,
                        ParentId = JobSeedIds.Alchemist,
                    }
                );
        }
    }
}
