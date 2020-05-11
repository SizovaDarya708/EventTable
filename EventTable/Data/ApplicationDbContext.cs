using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using EventTable.Models.Entities;

namespace EventTable.Data
{
    class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<User> Notes { get; set; }
        public DbSet<UserEvents> UserEvents { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(i=> new { i.ChatId, i.Login})
                .IsUnique();

            modelBuilder.Entity<UserEvents>()
                .HasKey(ue => new { ue.UserId, ue.EventId });

            modelBuilder.Entity<Note>()
                .HasOne<User>(s => s.User)
                .WithMany(g => g.Notes)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseNpgsql(AppSettings.DBConnection);
    }
}
