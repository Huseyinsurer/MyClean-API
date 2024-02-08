using Microsoft.EntityFrameworkCore;
using Domain.Models;
using MySql.EntityFrameworkCore.Extensions;
using System;
using Domain.Models.User;

namespace Infrastructure.Database
{
    public class ApiMainContext : DbContext
    {
        public ApiMainContext(DbContextOptions<ApiMainContext> options) : base(options)
        {
        }

        public DbSet<Dog> Dogs { get; set; }
        public DbSet<Cat> Cats { get; set; }
        public DbSet<Bird> Birds { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<Ownership> Ownerships { get; set; }

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
