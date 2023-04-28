using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TurbineRepair.Model;

namespace TurbineRepair.Migration;

public partial class TurbinerepairContext : DbContext
{
    public TurbinerepairContext()
    {
    }

    public TurbinerepairContext(DbContextOptions<TurbinerepairContext> options)
        : base(options)
    {
    }

    public virtual DbSet<UserDatum> UserData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=dpg-cgo56qd269v5rja2d8c0-a.frankfurt-postgres.render.com;Port=5432;Database=turbinerepair;Username=admin;Password=GZ22rodq238OjsYamXMtoFQpWEh8mQ98");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserData_pkey");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Login)
                .HasMaxLength(32)
                .IsFixedLength();
            entity.Property(e => e.Name)
                .HasMaxLength(32)
                .IsFixedLength();
            entity.Property(e => e.Password)
                .HasMaxLength(16)
                .IsFixedLength();
            entity.Property(e => e.Patronomyc)
                .HasMaxLength(32)
                .IsFixedLength();
            entity.Property(e => e.Phone)
                .HasMaxLength(12)
                .IsFixedLength();
            entity.Property(e => e.Surname)
                .HasMaxLength(32)
                .IsFixedLength();
            entity.Property(e => e.Pincode)
                .HasMaxLength(6)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
