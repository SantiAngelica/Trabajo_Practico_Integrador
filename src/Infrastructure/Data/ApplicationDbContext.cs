using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Enum;
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

        modelBuilder
            .Entity<Game>()
            .HasOne(g => g.Reservation)
            .WithOne(r => r.Game)
            .HasForeignKey<Reservation>(r => r.GameId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<UserComent>()
            .HasOne(uc => uc.User)
            .WithMany(u => u.UserComents)
            .HasForeignKey(uc => uc.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder
            .Entity<UserComent>()
            .HasOne(uc => uc.Commenter)
            .WithMany()
            .HasForeignKey(uc => uc.CommenterId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Game>().Navigation(g => g.Creator).AutoInclude();

        modelBuilder.Entity<Participation>().Navigation(p => p.Game).AutoInclude();
        modelBuilder.Entity<Participation>().Navigation(p => p.User).AutoInclude();

        modelBuilder.Entity<Property>().Navigation(p => p.Fields).AutoInclude();
        modelBuilder.Entity<Property>().Navigation(p => p.Schedules).AutoInclude();
        modelBuilder.Entity<Property>().Navigation(p => p.Owner).AutoInclude();
        modelBuilder.Entity<Property>().Navigation(p => p.Reservations).AutoInclude();

        modelBuilder
            .Entity<User>()
            .HasData(
                new User
                {
                    Id = 1,
                    Name = "muñeco",
                    Email = "muñeco@example.com",
                    Password = "1234", // 🔒 Solo para pruebas
                    Age = 30,
                    Zone = "Centro",
                    Role = RolesEnum.Player,
                },
                new User
                {
                    Id = 2,
                    Name = "Juan Pérez",
                    Email = "juan@example.com",
                    Password = "1234",
                    Age = 25,
                    Zone = "Norte",
                    Role = RolesEnum.Player,
                },
                new User
                {
                    Id = 3,
                    Name = "Adiur",
                    Email = "adiur@example.com",
                    Password = "1234",
                    Age = 28,
                    Zone = "Sur",
                    Role = RolesEnum.Admin,
                }
            );
    }
}
