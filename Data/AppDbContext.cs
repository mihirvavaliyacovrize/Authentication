//using Microsoft.EntityFrameworkCore;
//using JwtAuthAPI.Models;

//namespace JwtAuthAPI.Data;

//public class AppDbContext : DbContext
//{
//    public AppDbContext(
//     DbContextOptions<AppDbContext> options
//    ) : base(options)
//    {
//    }

//    public DbSet<User> Users { get; set; }

//    public DbSet<Job> Jobs { get; set; }
//}

using Microsoft.EntityFrameworkCore;
using JwtAuthAPI.Models;

namespace JwtAuthAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(
    DbContextOptions<AppDbContext> options
    ) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Job> Jobs { get; set; }


    protected override void OnModelCreating(
    ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Job>()
        .HasOne(j => j.CreatedByUser)
        .WithMany(u => u.JobsCreated)
        .HasForeignKey(j => j.CreatedBy);

        base.OnModelCreating(
         modelBuilder);
    }
}