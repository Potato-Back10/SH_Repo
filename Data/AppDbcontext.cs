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
}
