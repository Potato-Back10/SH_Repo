using Gamza.Models; 
using Microsoft.EntityFrameworkCore;

namespace Gamza.Extensions.Seed.Jobs
{
    public static class JobSeedRootExtensions
    {
        public static void SeedAllJobs(this ModelBuilder mb)
        {
            // 1. 공통 엔티티 구성
            mb.ConfigureJobEntity();

            // 2. 각 직업군별 데이터 추가
            mb.SeedWarriorBranch();
            mb.SeedThiefBranch();
            mb.SeedMageBranch();
            mb.SeedArcherBranch();
            mb.SeedHealerBranch();
        }

        public static void ConfigureJobEntity(this ModelBuilder mb)
        {
            mb.Entity<Job>(entity =>
            {
                entity.HasKey(j => j.Id);
                
                // Job 모델에 있는 프로퍼티 이름에 맞춰 수정했습니다.
                entity.Property(j => j.Name).IsRequired();
                
                // ★ 여기가 수정된 부분입니다 (ParentJob -> Parent / Children)
                entity.HasOne(j => j.Parent)          // 부모는 Parent
                      .WithMany(j => j.Children)      // 자식 목록은 Children
                      .HasForeignKey(j => j.ParentId) // 외래키는 ParentId
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}