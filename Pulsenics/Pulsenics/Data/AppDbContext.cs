using System;
using Microsoft.EntityFrameworkCore;
using Pulsenics.Models;

namespace Pulsenics.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Creating a many to many relationship between users and files
            modelBuilder.Entity<UserFile>()
                .HasKey(uf => new { uf.UserId, uf.FileId });

            modelBuilder.Entity<UserFile>()
                .HasOne(uf => uf.User)
                .WithMany(u => u.UserFiles)
                .HasForeignKey(uf => uf.UserId);

            modelBuilder.Entity<UserFile>()
                .HasOne(uf => uf.File)
                .WithMany(f => f.UserFiles)
                .HasForeignKey(uf => uf.FileId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Models.File> Files { get; set; }
        public DbSet<UserFile> UserFiles { get; set; }
    }
}