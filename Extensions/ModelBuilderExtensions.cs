using Gamza.Enums;
using Gamza.Models;
using Microsoft.EntityFrameworkCore;

namespace Gamza.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void SeedJobs(this ModelBuilder mb)
        {
            // 1차
            var warrior = new Job
            {
                Id = 1,
                Code = "warrior",
                Name = "워리어",
                Tier = JobTier.First,
                MinLevel = 10,
            };
            var thief = new Job
            {
                Id = 2,
                Code = "thief",
                Name = "도적",
                Tier = JobTier.First,
                MinLevel = 10,
            };
            var mage = new Job
            {
                Id = 3,
                Code = "mage",
                Name = "법사",
                Tier = JobTier.First,
                MinLevel = 10,
            };
            var archer = new Job
            {
                Id = 4,
                Code = "archer",
                Name = "궁수",
                Tier = JobTier.First,
                MinLevel = 10,
            };

            // 2차(워리어 가지)
            var knight = new Job
            {
                Id = 10,
                Code = "knight",
                Name = "나이트",
                Tier = JobTier.Second,
                MinLevel = 30,
                ParentId = warrior.Id,
            };
            var spearman = new Job
            {
                Id = 11,
                Code = "spearman",
                Name = "스피어맨",
                Tier = JobTier.Second,
                MinLevel = 30,
                ParentId = warrior.Id,
            };
            var ronin = new Job
            {
                Id = 12,
                Code = "ronin",
                Name = "로닌",
                Tier = JobTier.Second,
                MinLevel = 30,
                ParentId = warrior.Id,
            };

            // 3차(각 2차의 단일 진화)
            var paladin = new Job
            {
                Id = 20,
                Code = "paladin",
                Name = "팔라딘",
                Tier = JobTier.Third,
                MinLevel = 70,
                ParentId = knight.Id,
            };
            var dragonKnight = new Job
            {
                Id = 21,
                Code = "dragon_knight",
                Name = "드래곤 나이트",
                Tier = JobTier.Third,
                MinLevel = 70,
                ParentId = spearman.Id,
            };
            var blade = new Job
            {
                Id = 22,
                Code = "blade",
                Name = "블레이드",
                Tier = JobTier.Third,
                MinLevel = 70,
                ParentId = ronin.Id,
            };

            mb.Entity<Job>()
                .HasData(
                    warrior,
                    thief,
                    mage,
                    archer,
                    knight,
                    spearman,
                    ronin,
                    paladin,
                    dragonKnight,
                    blade
                );
        }
    }
}
