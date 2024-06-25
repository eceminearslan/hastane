using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Hastane.Models;

public partial class HastaneContext : DbContext
{
    public HastaneContext()
    {
    }

    public HastaneContext(DbContextOptions<HastaneContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Branslar> Branslars { get; set; }

    public virtual DbSet<Doktorlar> Doktorlars { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ECEARSLAN;Database=Hastane;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Branslar>(entity =>
        {
            entity.HasKey(e => e.BransId).HasName("PK_dbo.hastane");

            entity.ToTable("Branslar");

            entity.Property(e => e.BransAd)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Doktorlar>(entity =>
        {
            entity.HasKey(e => e.DoktorId).HasName("PK_doktor");

            entity.ToTable("Doktorlar");

            entity.Property(e => e.DoktorAdSoyad)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Brans).WithMany(p => p.Doktorlars)
                .HasForeignKey(d => d.BransId)
                .HasConstraintName("FK_doktor_branşlar");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
