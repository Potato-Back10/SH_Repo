using Gamza.Enums;
using Gamza.Models;
using Microsoft.EntityFrameworkCore;

namespace Gamza.Extensions.Seed.Jobs
{
    internal static class MageSeedExtensions
    {
        public static void SeedMageBranch(this ModelBuilder mb)
        {
            mb.Entity<Job>()
                .HasData(
                    // 1차
                    new Job
                    {
                        Id = JobSeedIds.Mage,
                        Code = "mage",
                        Name = "법사",
                        Tier = JobTier.First,
                        MinLevel = 10,
                    },
                    // 2차
                    new Job
                    {
                        Id = JobSeedIds.ArchMage,
                        Code = "archMage",
                        Name = "아크메이지",
                        Tier = JobTier.Second,
                        MinLevel = 30,
                        ParentId = JobSeedIds.Mage,
                    },
                    new Job
                    {
                        Id = JobSeedIds.DarkMage,
                        Code = "darkMage",
                        Name = "흑마법사",
                        Tier = JobTier.Second,
                        MinLevel = 30,
                        ParentId = JobSeedIds.Mage,
                    },
                    new Job
                    {
                        Id = JobSeedIds.TimeMage,
                        Code = "timeMage",
                        Name = "시간법사",
                        Tier = JobTier.Second,
                        MinLevel = 30,
                        ParentId = JobSeedIds.Mage,
                    },
                    // 3차
                    new Job
                    {
                        Id = JobSeedIds.GrandSorcerer,
                        Code = "grandsorcerer",
                        Name = "그랜드소서러",
                        Tier = JobTier.Third,
                        MinLevel = 70,
                        ParentId = JobSeedIds.ArchMage,
                    },
                    new Job
                    {
                        Id = JobSeedIds.NecroMancer,
                        Code = "necromancer",
                        Name = "네크로맨서",
                        Tier = JobTier.Third,
                        MinLevel = 70,
                        ParentId = JobSeedIds.DarkMage,
                    },
                    new Job
                    {
                        Id = JobSeedIds.ChronoTrigger,
                        Code = "chronotrigger",
                        Name = "크로노트리거",
                        Tier = JobTier.Third,
                        MinLevel = 70,
                        ParentId = JobSeedIds.TimeMage,
                    }
                );
        }
    }
}
