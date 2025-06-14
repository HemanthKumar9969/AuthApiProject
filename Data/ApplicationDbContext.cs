using AuthApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } // DbSet for your User model

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure unique constraint for Username and Email to prevent duplicates
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // You could add initial data (seeding) here if desired,
            // but for this project, we'll rely on registration.
        }
    }
}