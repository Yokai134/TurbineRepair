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

    public virtual DbSet<Deportament> Deportaments { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<UserDatum> UserData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=dpg-cgo56qd269v5rja2d8c0-a.frankfurt-postgres.render.com;Port=5432;Database=turbinerepair;Username=admin;Password=GZ22rodq238OjsYamXMtoFQpWEh8mQ98");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Deportament>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Deportament_pkey");

            entity.ToTable("Deportament");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DeportamentName).HasColumnType("character varying");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Post_pkey");

            entity.ToTable("Post");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DeportamentId).HasColumnName("DeportamentID");
            entity.Property(e => e.PostName).HasMaxLength(55);

            entity.HasOne(d => d.Deportament).WithMany(p => p.Posts)
                .HasForeignKey(d => d.DeportamentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DeportamentID_Post_fk");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Role_pkey");

            entity.ToTable("Role");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.RoleName).HasMaxLength(32);
        });

        modelBuilder.Entity<UserDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserData_pkey");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Login).HasMaxLength(32);
            entity.Property(e => e.Name).HasMaxLength(32);
            entity.Property(e => e.Password).HasMaxLength(16);
            entity.Property(e => e.Patronomyc).HasMaxLength(32);
            entity.Property(e => e.Phone).HasMaxLength(12);
            entity.Property(e => e.Pincode).HasMaxLength(6);
            entity.Property(e => e.Surname).HasMaxLength(32);

            entity.HasOne(d => d.PostNavigation).WithMany(p => p.UserData)
                .HasForeignKey(d => d.Post)
                .HasConstraintName("Post_fk");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.UserData)
                .HasForeignKey(d => d.Role)
                .HasConstraintName("Role_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
