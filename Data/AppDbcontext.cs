using Gamza.Models;
using Microsoft.EntityFrameworkCore;

namespace Gamza.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; } = null!;
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
