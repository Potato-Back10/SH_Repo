using Gamza.Extensions.Seed.Jobs;
using Gamza.Models;
using Microsoft.EntityFrameworkCore;

namespace Gamza.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    // ==========================================
    // 1. DbSet 정의 (중복 제거 및 정리 완료)
    // ==========================================
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Job> Jobs => Set<Job>();

    // Players는 하나만 남김 (다른 DbSet과 스타일 통일)
    public DbSet<Player> Players { get; set; } = null!;

    public DbSet<PlayerJobHistory> PlayerJobHistories => Set<PlayerJobHistory>();

    public DbSet<BattleSession> Battles { get; set; } = null!;
    public DbSet<BattleParticipant> BattleParticipants { get; set; } = null!;
    public DbSet<CombatLog> CombatLogs { get; set; } = null!;
    public DbSet<RewardGrant> RewardGrants { get; set; } = null!;
    public DbSet<Monster> Monsters { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.SeedAllJobs();

        modelBuilder.Entity<BattleParticipant>()
            .HasOne(p => p.Battle)
            .WithMany(s => s.Participants)
            .HasForeignKey(p => p.BattleId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<BattleParticipant>()
            .HasIndex(l => l.BattleId);

        modelBuilder.Entity<RewardGrant>()
            .HasIndex(r => new { r.BattleId, r.PlayerId })
            .IsUnique();

        modelBuilder.Entity<Player>()
            .HasOne(p => p.User)
            .WithMany(u => u.Players)
            .HasForeignKey(p => p.UserId);

        modelBuilder.Entity<Player>()
            .HasIndex(p => p.NickName)
            .IsUnique();
    }
}