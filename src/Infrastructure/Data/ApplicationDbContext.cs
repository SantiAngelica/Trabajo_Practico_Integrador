using System;
using System.Collections.Generic;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public partial class ApplicationDbContext : DbContext
{
    public DbSet<Game> Games { get; set; }

    public DbSet<Property> Propertys { get; set; }

    public DbSet<Field> Fields { get; set; }

    public DbSet<Reservation> Reservations { get; set; }

    public DbSet<Schedule> Schedules { get; set; }

    public DbSet<Participation> Participations { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<UserField> UserFields { get; set; }
    public DbSet<UserPosition> UserPositions { get; set; }
    public DbSet<UserComent> UserComents { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Game>()
            .HasOne(g => g.Creator)
            .WithMany(u => u.GamesCreated)
            .HasForeignKey(g => g.CreatorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Game>().Navigation(g => g.Creator).AutoInclude();

        modelBuilder.Entity<Participation>().Navigation(p => p.Game).AutoInclude();
        modelBuilder.Entity<Participation>().Navigation(p => p.User).AutoInclude();

        modelBuilder.Entity<Property>().Navigation(p => p.Fields).AutoInclude();
        modelBuilder.Entity<Property>().Navigation(p => p.Schedules).AutoInclude();
        modelBuilder.Entity<Property>().Navigation(p => p.Owner).AutoInclude();
    }
}
