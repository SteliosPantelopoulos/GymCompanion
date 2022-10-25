using System;
using System.Collections.Generic;
using GymCompanion.WebServices.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GymCompanion.WebServices.DAL
{
    public partial class GymCompanionContext : IdentityDbContext<User>
    {
        public GymCompanionContext()
        {
        }

        public GymCompanionContext(DbContextOptions<GymCompanionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BodyPart> BodyParts { get; set; } = null!;
        public virtual DbSet<Exercise> Exercises { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Workout> Workouts { get; set; } = null!;
        public virtual DbSet<UserExercise> UserExercises { get; set; } = null;
        public virtual DbSet<Set> Sets { get; set; } = null;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Initial Catalog=GymCompanion; User ID = sa; Password = P@ssw0rd;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BodyPart>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_BodyPartId");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasIndex(e => e.Name, "BodyPart_Name_Unique")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Exercise>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_ExerciseId");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasIndex(e => e.Name, "Exercise_Name_Unique")
                    .IsUnique();

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne<BodyPart>(e => e.BodyPart)
                    .WithMany(bp => bp.Exercises)
                    .HasForeignKey(e => e.BodyPartId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Workout>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_WorkoutId");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasMany(w => w.Exercises)
                    .WithMany(p => p.Workouts);
            });

            modelBuilder.Entity<Set>(entity =>
            {
                entity.HasKey(s => s.Id)
                    .HasName("PK_SetId");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

            });

            modelBuilder.Entity<UserExercise>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_UserExerciseId");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasMany<Set>(ue => ue.Sets)
                    .WithOne(s => s.UserExercise)
                    .HasForeignKey(s => s.UserExerciseId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne<Exercise>(ue => ue.Exercise)
                    .WithMany(e => e.UserExercises)
                    .HasForeignKey(ue => ue.ExerciseId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
