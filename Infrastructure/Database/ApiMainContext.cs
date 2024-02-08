using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Domain.Models.User;
using System;

namespace Infrastructure.Database
{
    public class ApiMainContext : DbContext
    {
        public DbSet<Dog> Dogs { get; set; }
        public DbSet<Cat> Cats { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<Ownership> Ownerships { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=ApiMainContext.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ownership>().HasKey(o => new { o.UserId, o.AnimalId });

            modelBuilder.Entity<Ownership>()
                .HasOne(o => o.User)
                .WithMany(u => u.Ownerships)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ownership>()
                .HasOne(o => o.Animal)
                .WithMany(a => a.Ownerships)
                .HasForeignKey(o => o.AnimalId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
