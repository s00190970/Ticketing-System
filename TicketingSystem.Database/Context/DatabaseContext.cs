using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketingSystem.Database.Entities;

namespace TicketingSystem.Database.Context
{
    public class DatabaseContext : IdentityDbContext<User>
    {
        public virtual DbSet<Priority> Priorities { get; set; }
        public virtual DbSet<ServiceType> ServiceTypes { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<TicketType> TicketTypes { get; set; }
        public new virtual DbSet<User> Users { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(m => m.Tickets)
                .WithOne(o => o.User);

            modelBuilder.Entity<Status>().HasIndex(s => s.Name).IsUnique();
            modelBuilder.Entity<Priority>().HasIndex(s => s.Name).IsUnique();
            modelBuilder.Entity<ServiceType>().HasIndex(s => s.Name).IsUnique();
            modelBuilder.Entity<TicketType>().HasIndex(s => s.Name).IsUnique();
            modelBuilder.Entity<User>().HasIndex(s => s.UserName).IsUnique();
        }
    }
}
