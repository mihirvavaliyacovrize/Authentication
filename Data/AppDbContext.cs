

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
        .HasForeignKey(j => j.CreatedBy)
        .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<Job>()
        .HasOne(j => j.Photographer)
        .WithMany(u => u.PhotographerJobs)
        .HasForeignKey(j => j.PhotographerId)
        .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(
        modelBuilder);
    }
}