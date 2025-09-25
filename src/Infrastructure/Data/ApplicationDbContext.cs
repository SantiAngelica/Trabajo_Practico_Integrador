using System;
using System.Collections.Generic;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<GameAplication> GameAplications { get; set; }

    public virtual DbSet<GameInvitation> GameInvitations { get; set; }

    public virtual DbSet<GameUser> GameUsers { get; set; }

    public virtual DbSet<Property> Propertys { get; set; }

    public virtual DbSet<PropertyTypeField> PropertyTypeFields { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<ScheduleProperty> ScheduleProperties { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserComent> UserComents { get; set; }

    public virtual DbSet<UserField> UserFields { get; set; }

    public virtual DbSet<UserPosition> UserPositions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>(entity =>
        {
            entity.ToTable("games");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdUserCreator).HasColumnName("id_user_creator");
            entity.Property(e => e.MissingPlayers).HasColumnName("missing_players");

            entity
                .HasOne(d => d.IdUserCreatorNavigation)
                .WithMany(p => p.Games)
                .HasForeignKey(d => d.IdUserCreator);
        });

        modelBuilder.Entity<GameAplication>(entity =>
        {
            entity.ToTable("game_aplications");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdGame).HasColumnName("id_game");
            entity.Property(e => e.IdUserApplicant).HasColumnName("id_user_applicant");
            entity.Property(e => e.State).HasDefaultValue("pendiente").HasColumnName("state");

            entity
                .HasOne(d => d.IdGameNavigation)
                .WithMany(p => p.GameAplications)
                .HasForeignKey(d => d.IdGame);

            entity
                .HasOne(d => d.IdUserApplicantNavigation)
                .WithMany(p => p.GameAplications)
                .HasForeignKey(d => d.IdUserApplicant);
        });

        modelBuilder.Entity<GameInvitation>(entity =>
        {
            entity.ToTable("game_invitations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdGame).HasColumnName("id_game");
            entity.Property(e => e.IdUserReciever).HasColumnName("id_user_reciever");
            entity.Property(e => e.State).HasDefaultValue("pendiente").HasColumnName("state");

            entity
                .HasOne(d => d.IdGameNavigation)
                .WithMany(p => p.GameInvitations)
                .HasForeignKey(d => d.IdGame);

            entity
                .HasOne(d => d.IdUserRecieverNavigation)
                .WithMany(p => p.GameInvitations)
                .HasForeignKey(d => d.IdUserReciever);
        });

        modelBuilder.Entity<GameUser>(entity =>
        {
            entity.ToTable("game_users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdGame).HasColumnName("id_game");
            entity.Property(e => e.IdUser).HasColumnName("id_user");

            entity
                .HasOne(d => d.IdGameNavigation)
                .WithMany(p => p.GameUsers)
                .HasForeignKey(d => d.IdGame);

            entity
                .HasOne(d => d.IdUserNavigation)
                .WithMany(p => p.GameUsers)
                .HasForeignKey(d => d.IdUser);
        });

        modelBuilder.Entity<Property>(entity =>
        {
            entity.ToTable("propertys");

            entity.Property(e => e.Id).ValueGeneratedNever().HasColumnName("id");
            entity.Property(e => e.Adress).HasColumnType("VARCHAR(200)").HasColumnName("adress");
            entity.Property(e => e.IdUserOwner).HasColumnName("id_user_owner");
            entity.Property(e => e.Name).HasColumnType("VARCHAR(200)").HasColumnName("name");
            entity.Property(e => e.Zone).HasColumnType("VARCHAR(200)").HasColumnName("zone");

            entity
                .HasOne(d => d.IdUserOwnerNavigation)
                .WithMany(p => p.Properties)
                .HasForeignKey(d => d.IdUserOwner);
        });

        modelBuilder.Entity<PropertyTypeField>(entity =>
        {
            entity.ToTable("property_type_fields");

            entity.Property(e => e.Id).ValueGeneratedNever().HasColumnName("id");
            entity.Property(e => e.FieldType).HasColumnName("field_type");
            entity.Property(e => e.IdProperty).HasColumnName("id_property");

            entity
                .HasOne(d => d.IdPropertyNavigation)
                .WithMany(p => p.PropertyTypeFields)
                .HasForeignKey(d => d.IdProperty);
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.ToTable("reservations");

            entity.Property(e => e.Id).ValueGeneratedNever().HasColumnName("id");
            entity.Property(e => e.Date).HasColumnType("DATE").HasColumnName("date");
            entity.Property(e => e.IdField).HasColumnName("id_field");
            entity.Property(e => e.IdGame).HasColumnName("id_game");
            entity.Property(e => e.IdSchedule).HasColumnName("id_schedule");
            entity.Property(e => e.State).HasColumnName("state");

            entity
                .HasOne(d => d.IdFieldNavigation)
                .WithMany(p => p.Reservations)
                .HasForeignKey(d => d.IdField);

            entity
                .HasOne(d => d.IdGameNavigation)
                .WithMany(p => p.Reservations)
                .HasForeignKey(d => d.IdGame);

            entity
                .HasOne(d => d.IdScheduleNavigation)
                .WithMany(p => p.Reservations)
                .HasForeignKey(d => d.IdSchedule);
        });

        modelBuilder.Entity<ScheduleProperty>(entity =>
        {
            entity.ToTable("schedule_properties");

            entity.Property(e => e.Id).ValueGeneratedNever().HasColumnName("id");
            entity.Property(e => e.IdProperty).HasColumnName("id_property");
            entity.Property(e => e.Schedule).HasColumnName("schedule");

            entity
                .HasOne(d => d.IdPropertyNavigation)
                .WithMany(p => p.ScheduleProperties)
                .HasForeignKey(d => d.IdProperty);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "IX_users_email").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Email).HasColumnType("VARCHAR(255)").HasColumnName("email");
            entity.Property(e => e.Name).HasColumnType("VARCHAR(255)").HasColumnName("name");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Rol).HasDefaultValue("player").HasColumnName("rol");
            entity.Property(e => e.Zone).HasColumnType("VARCHAR(255)").HasColumnName("zone");
        });

        modelBuilder.Entity<UserComent>(entity =>
        {
            entity.ToTable("user_coments");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Body).HasColumnName("body");
            entity.Property(e => e.IdUserCommenter).HasColumnName("id_user_commenter");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserComents).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<UserField>(entity =>
        {
            entity.ToTable("user_fields");

            entity.Property(e => e.Id).ValueGeneratedNever().HasColumnName("id");
            entity.Property(e => e.Field).HasColumnName("field");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserFields).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<UserPosition>(entity =>
        {
            entity.ToTable("user_positions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity
                .Property(e => e.Position)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("position");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserPositions).HasForeignKey(d => d.UserId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
