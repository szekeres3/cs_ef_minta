using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DinoData.Model;

public partial class JurassicDbContext : DbContext
{
    public JurassicDbContext()
    {
    }

    public JurassicDbContext(DbContextOptions<JurassicDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Dinosaur> Dinosaurs { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<LocationDinosaur> LocationDinosaurs { get; set; }

    public virtual DbSet<Species> Species { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=database/jurassicDb.sqlite");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dinosaur>(entity =>
        {
            entity.ToTable("dinosaurs");

            entity.HasIndex(e => e.Nickname, "IX_dinosaurs_nickname").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Birth).HasColumnName("birth");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.Length).HasColumnName("length");
            entity.Property(e => e.Nickname).HasColumnName("nickname");
            entity.Property(e => e.SpeciesId).HasColumnName("species_id");
            entity.Property(e => e.Version)
                .HasDefaultValue("??")
                .HasColumnName("version");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.Species).WithMany(p => p.Dinosaurs).HasForeignKey(d => d.SpeciesId);
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.ToTable("locations");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<LocationDinosaur>(entity =>
        {
            entity.ToTable("location_dinosaur");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.DinosaurId).HasColumnName("dinosaur_id");
            entity.Property(e => e.LocationId).HasColumnName("location_id");

            entity.HasOne(d => d.Dinosaur).WithMany(p => p.LocationDinosaurs).HasForeignKey(d => d.DinosaurId);

            entity.HasOne(d => d.Location).WithMany(p => p.LocationDinosaurs).HasForeignKey(d => d.LocationId);
        });

        modelBuilder.Entity<Species>(entity =>
        {
            entity.ToTable("species");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
