using Gamza.Extensions.Seed.Jobs;
using Gamza.Models;
using Microsoft.EntityFrameworkCore;

namespace Gamza.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Job> Jobs => Set<Job>();
    public DbSet<Player> Players => Set<Player>();
    public DbSet<PlayerJobHistory> PlayerJobHistories => Set<PlayerJobHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ★ 가지별 시드와 자기참조 매핑까지 한 번에
        modelBuilder.SeedAllJobs();
    }

    public DbSet<BattleSession> Battles { get; set; } = null!;
    public DbSet<BattleParticipant> BattleParticipants { get; set; } = null!;
    public DbSet<CombatLog> CombatLogs { get; set; } = null!;
    public DbSet<RewardGrant> RewardGrants { get; set; } = null!;
    public DbSet<Player> Players { get; set; } = null!;
    public DbSet<Monster> Monsters { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder b)
    {
        base.OnModelCreating(b);

        b.Entity<BattleParticipant>()
            .HasOne(p => p.Battle)
            .WithMany(s => s.Participants)
            .HasForeignKey(p => p.BattleId)
            .OnDelete(DeleteBehavior.Cascade);

        b.Entity<BattleParticipant>().HasIndex(l => l.BattleId);

        b.Entity<RewardGrant>().HasIndex(r => new { r.BattleId, r.PlayerId }).IsUnique();
    }
    public DbSet<Player> Players { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //  User 1 : N Player
        modelBuilder.Entity<Player>()
            .HasOne(p => p.User)
            .WithMany(u => u.Players)
            .HasForeignKey(p => p.UserId);

        modelBuilder.Entity<Player>()
        .HasIndex(p => p.NickName)
        .IsUnique();
    }
}
