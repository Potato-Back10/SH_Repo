using Gamza.Enums;
using Gamza.Models;
using Microsoft.EntityFrameworkCore;

namespace Gamza.Extensions.Seed.Jobs
{
    internal static class ThiefSeedExtensions
    {
        public static void SeedThiefBranch(this ModelBuilder mb)
        {
            mb.Entity<Job>()
                .HasData(
                    // 1차 루트 (이미 있다면 중복 정의 X — 여기선 안전하게 재명시해도 동일 값이면 문제 없음)
                    new Job
                    {
                        Id = JobSeedIds.Thief,
                        Code = "thief",
                        Name = "도적",
                        Tier = JobTier.First,
                        MinLevel = 10,
                    },
                    // 2차
                    new Job
                    {
                        Id = JobSeedIds.Assassin,
                        Code = "assassin",
                        Name = "어쌔신",
                        Tier = JobTier.Second,
                        MinLevel = 30,
                        ParentId = JobSeedIds.Thief,
                    },
                    new Job
                    {
                        Id = JobSeedIds.ShadowDancer,
                        Code = "shadowdancer",
                        Name = "쉐도우댄서",
                        Tier = JobTier.Second,
                        MinLevel = 30,
                        ParentId = JobSeedIds.Thief,
                    },
                    new Job
                    {
                        Id = JobSeedIds.Phantom,
                        Code = "phantom",
                        Name = "팬텀",
                        Tier = JobTier.Second,
                        MinLevel = 30,
                        ParentId = JobSeedIds.Thief,
                    },
                    // 3차
                    new Job
                    {
                        Id = JobSeedIds.Reaper,
                        Code = "reaper",
                        Name = "리퍼",
                        Tier = JobTier.Third,
                        MinLevel = 70,
                        ParentId = JobSeedIds.Assassin,
                    },
                    new Job
                    {
                        Id = JobSeedIds.Specter,
                        Code = "specter",
                        Name = "스펙터",
                        Tier = JobTier.Third,
                        MinLevel = 70,
                        ParentId = JobSeedIds.ShadowDancer,
                    },
                    new Job
                    {
                        Id = JobSeedIds.Raven,
                        Code = "raven",
                        Name = "레이븐",
                        Tier = JobTier.Third,
                        MinLevel = 70,
                        ParentId = JobSeedIds.Phantom,
                    }
                );
        }
    }
}
