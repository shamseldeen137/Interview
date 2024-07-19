using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Interview.Models;

public partial class InterviewContext : DbContext
{
    public InterviewContext()
    {
    }

    public InterviewContext(DbContextOptions<InterviewContext> options)
        : base(options)
    {
    }

    public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=.;Database=Interview;MultipleActiveResultSets=true;Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.HasKey(e => e.LineNumber).HasName("PK__InvoiceD__26075CEB5A7EC385");

            entity.Property(e => e.LineNumber).HasColumnName("lineNumber");
            entity.Property(e => e.ExpiryDate)
                .HasColumnType("datetime")
                .HasColumnName("expiryDate");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("price");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .HasColumnName("productName");
            entity.Property(e => e.Quantity)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("quantity");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("total");

            entity.HasOne(d => d.UnitNoNavigation).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.UnitNo)
                .HasConstraintName("FK__InvoiceDe__UnitN__3D5E1FD2");
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.UnitNo).HasName("PK__Unit__55D65B268E950D19");

            entity.ToTable("Unit");

            entity.Property(e => e.UnitNo).HasColumnName("unitNo");
            entity.Property(e => e.UnitName)
                .HasMaxLength(100)
                .HasColumnName("unitName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
