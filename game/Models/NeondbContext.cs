using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace game.Models;

public partial class NeondbContext : DbContext
{
    public NeondbContext()
    {
    }

    public NeondbContext(DbContextOptions<NeondbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<GameManagement> GameManagements { get; set; }

    public virtual DbSet<GamerManagement> GamerManagements { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=ep-fragrant-field-a4dxxb5d-pooler.us-east-1.aws.neon.tech;Database=neondb;Username=neondb_owner;Password=npg_b8SmHdYRq2Bu");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GameManagement>(entity =>
        {
            entity.HasKey(e => e.GameId).HasName("game_management_pkey");

            entity.ToTable("game_management");

            entity.Property(e => e.GameId).HasColumnName("game_id");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.GameName)
                .HasMaxLength(100)
                .HasColumnName("game_name");
        });

        modelBuilder.Entity<GamerManagement>(entity =>
        {
            entity.HasKey(e => e.GamerId).HasName("gamer_management_pkey");

            entity.ToTable("gamer_management");

            entity.Property(e => e.GamerId).HasColumnName("gamer_id");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.GamerName)
                .HasMaxLength(100)
                .HasColumnName("gamer_name");

            entity.HasMany(d => d.Games).WithMany(p => p.Gamers)
                .UsingEntity<Dictionary<string, object>>(
                    "GamerGameRelation",
                    r => r.HasOne<GameManagement>().WithMany()
                        .HasForeignKey("GameId")
                        .HasConstraintName("gamer_game_relation_game_id_fkey"),
                    l => l.HasOne<GamerManagement>().WithMany()
                        .HasForeignKey("GamerId")
                        .HasConstraintName("gamer_game_relation_gamer_id_fkey"),
                    j =>
                    {
                        j.HasKey("GamerId", "GameId").HasName("gamer_game_relation_pkey");
                        j.ToTable("gamer_game_relation");
                        j.IndexerProperty<int>("GamerId").HasColumnName("gamer_id");
                        j.IndexerProperty<int>("GameId").HasColumnName("game_id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
