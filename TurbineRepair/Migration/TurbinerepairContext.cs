using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Deportament> Deportaments { get; set; }

    public virtual DbSet<MessageList> MessageLists { get; set; }

    public virtual DbSet<Oraganization> Oraganizations { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<ProjectDatum> ProjectData { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<StatusProject> StatusProjects { get; set; }

    public virtual DbSet<Turbine> Turbines { get; set; }

    public virtual DbSet<TurbineImage> TurbineImages { get; set; }

    public virtual DbSet<TurbineMdp> TurbineMdps { get; set; }

    public virtual DbSet<TurbineMdum> TurbineMda { get; set; }

    public virtual DbSet<TurbineNotification> TurbineNotifications { get; set; }

    public virtual DbSet<TurbinePgp> TurbinePgps { get; set; }

    public virtual DbSet<TurbineRequest> TurbineRequests { get; set; }

    public virtual DbSet<TurbineScpg> TurbineScpgs { get; set; }

    public virtual DbSet<TypeOfWork> TypeOfWorks { get; set; }

    public virtual DbSet<UserDatum> UserData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=dpg-cgo56qd269v5rja2d8c0-a.frankfurt-postgres.render.com;Port=5432;Database=Test;Username=admin;Password=GZ22rodq238OjsYamXMtoFQpWEh8mQ98");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Customer_pkey");

            entity.ToTable("Customer");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CustomerName).HasMaxLength(32);
            entity.Property(e => e.CustomerPatronomyc).HasMaxLength(32);
            entity.Property(e => e.CustomerSurname).HasMaxLength(32);

            entity.HasOne(d => d.CustomerOrganizationNavigation).WithMany(p => p.Customers)
                .HasForeignKey(d => d.CustomerOrganization)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Oraganization_fk");
        });

        modelBuilder.Entity<Deportament>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Deportament_pkey");

            entity.ToTable("Deportament");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DeportamentName).HasColumnType("character varying");
        });

        modelBuilder.Entity<MessageList>(entity =>
        {
            entity.HasKey(e => e.MessageListId).HasName("MessageList_pkey");

            entity.ToTable("MessageList");

            entity.Property(e => e.MessageListId).HasColumnName("MessageListID");
            entity.Property(e => e.MessgeTime).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.MessageReceiptNavigation).WithMany(p => p.MessageListMessageReceiptNavigations)
                .HasForeignKey(d => d.MessageReceipt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ReceiptUser_fk");

            entity.HasOne(d => d.MessageSenderNavigation).WithMany(p => p.MessageListMessageSenderNavigations)
                .HasForeignKey(d => d.MessageSender)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SenderUser_fk");
        });

        modelBuilder.Entity<Oraganization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Oraganization_pkey");

            entity.ToTable("Oraganization");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.OraganizationAdres).HasMaxLength(55);
            entity.Property(e => e.OraganizationName).HasMaxLength(55);
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

        modelBuilder.Entity<ProjectDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProjectData_pkey");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ProjectName).HasMaxLength(255);

            entity.HasOne(d => d.ProjectCustomerNavigation).WithMany(p => p.ProjectData)
                .HasForeignKey(d => d.ProjectCustomer)
                .HasConstraintName("Customer_fk");

            entity.HasOne(d => d.ProjectExecutorNavigation).WithMany(p => p.ProjectDatumProjectExecutorNavigations)
                .HasForeignKey(d => d.ProjectExecutor)
                .HasConstraintName("UserData_Executor_fk");

            entity.HasOne(d => d.ProjectSecondExecutorNavigation).WithMany(p => p.ProjectDatumProjectSecondExecutorNavigations)
                .HasForeignKey(d => d.ProjectSecondExecutor)
                .HasConstraintName("SecondExecutor_fk");

            entity.HasOne(d => d.ProjectStatusNavigation).WithMany(p => p.ProjectData)
                .HasForeignKey(d => d.ProjectStatus)
                .HasConstraintName("StatusProject_fk");

            entity.HasOne(d => d.ProjectTurbineNavigation).WithMany(p => p.ProjectData)
                .HasForeignKey(d => d.ProjectTurbine)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Turbine_fk");

            entity.HasOne(d => d.TypeProjectNavigation).WithMany(p => p.ProjectData)
                .HasForeignKey(d => d.TypeProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TypeOfWork_fk");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Role_pkey");

            entity.ToTable("Role");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.RoleName).HasMaxLength(32);
        });

        modelBuilder.Entity<StatusProject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("StatusProject_pkey");

            entity.ToTable("StatusProject");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.StatusName).HasMaxLength(55);
        });

        modelBuilder.Entity<Turbine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Turbine_pkey");

            entity.ToTable("Turbine");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.TurbineCost).HasPrecision(15, 4);
            entity.Property(e => e.TurbineMda).HasColumnName("TurbineMDA");
            entity.Property(e => e.TurbineMdp).HasColumnName("TurbineMDP");
            entity.Property(e => e.TurbineName).HasMaxLength(30);
            entity.Property(e => e.TurbinePgp).HasColumnName("TurbinePGP");
            entity.Property(e => e.TurbineScpg).HasColumnName("TurbineSCPG");

            entity.HasOne(d => d.TurbineImageNavigation).WithMany(p => p.Turbines)
                .HasForeignKey(d => d.TurbineImage)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Turbine_Image_fk");

            entity.HasOne(d => d.TurbineMdaNavigation).WithMany(p => p.Turbines)
                .HasForeignKey(d => d.TurbineMda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Turbine_MDA_fk");

            entity.HasOne(d => d.TurbineMdpNavigation).WithMany(p => p.Turbines)
                .HasForeignKey(d => d.TurbineMdp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Turbine_MDP_fk");

            entity.HasOne(d => d.TurbinePgpNavigation).WithMany(p => p.Turbines)
                .HasForeignKey(d => d.TurbinePgp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Turbine_PGP_fk");

            entity.HasOne(d => d.TurbineScpgNavigation).WithMany(p => p.Turbines)
                .HasForeignKey(d => d.TurbineScpg)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Turbine_SCPG_fk");
        });

        modelBuilder.Entity<TurbineImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TurbineImage_pkey");

            entity.ToTable("TurbineImage");

            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<TurbineMdp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TurbineMDP_pkey");

            entity.ToTable("TurbineMDP");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Height).HasMaxLength(32);
            entity.Property(e => e.Lenght).HasMaxLength(32);
            entity.Property(e => e.Weight).HasMaxLength(32);
            entity.Property(e => e.Width).HasMaxLength(32);
        });

        modelBuilder.Entity<TurbineMdum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TurbineMDA_pkey");

            entity.ToTable("TurbineMDA");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DriveShaftSpeed).HasMaxLength(32);
            entity.Property(e => e.Efficiency).HasMaxLength(32);
            entity.Property(e => e.Emissions).HasMaxLength(32);
            entity.Property(e => e.ExhaustMassFlow).HasMaxLength(32);
            entity.Property(e => e.ExhaustTemperature).HasMaxLength(32);
            entity.Property(e => e.Fuel).HasMaxLength(100);
            entity.Property(e => e.HeatRate).HasMaxLength(32);
            entity.Property(e => e.PowerOutput).HasMaxLength(32);
            entity.Property(e => e.PressureRatio).HasMaxLength(32);
        });

        modelBuilder.Entity<TurbineNotification>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TurbineNotification");

            entity.Property(e => e.TurbineNotificationDateTime).HasColumnType("timestamp without time zone");
            entity.Property(e => e.TurbineNotificationId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<TurbinePgp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TurbinePGP_pkey");

            entity.ToTable("TurbinePGP");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Height).HasMaxLength(32);
            entity.Property(e => e.Lenght).HasMaxLength(32);
            entity.Property(e => e.Weight).HasMaxLength(32);
            entity.Property(e => e.Width).HasMaxLength(32);
        });

        modelBuilder.Entity<TurbineRequest>(entity =>
        {
            entity.HasKey(e => e.TurbineRequestId).HasName("TurbineRequest_pkey");

            entity.ToTable("TurbineRequest");

            entity.HasOne(d => d.TurbineRequestExecutorNavigation).WithMany(p => p.TurbineRequests)
                .HasForeignKey(d => d.TurbineRequestExecutor)
                .HasConstraintName("TurbineRepairExecutor_fk");

            entity.HasOne(d => d.TurbineRequestProjectNavigation).WithMany(p => p.TurbineRequests)
                .HasForeignKey(d => d.TurbineRequestProject)
                .HasConstraintName("TurbineRequestProject_fk");

            entity.HasOne(d => d.TurbineRequestStatusNavigation).WithMany(p => p.TurbineRequests)
                .HasForeignKey(d => d.TurbineRequestStatus)
                .HasConstraintName("TurbineRequestStatus_fk");

            entity.HasOne(d => d.TurbineRequestTypeOfWorkNavigation).WithMany(p => p.TurbineRequests)
                .HasForeignKey(d => d.TurbineRequestTypeOfWork)
                .HasConstraintName("TurbineRepairTypeOfWork_fk");
        });

        modelBuilder.Entity<TurbineScpg>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TurbineSCPG_pkey");

            entity.ToTable("TurbineSCPG");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Emissions).HasMaxLength(32);
            entity.Property(e => e.ExhaustMassFlow).HasMaxLength(32);
            entity.Property(e => e.ExhaustTemperature).HasMaxLength(32);
            entity.Property(e => e.Frequency).HasMaxLength(32);
            entity.Property(e => e.Fuel).HasMaxLength(100);
            entity.Property(e => e.GrossEfficiency).HasMaxLength(32);
            entity.Property(e => e.HeatRate).HasMaxLength(32);
            entity.Property(e => e.PowerOutput).HasMaxLength(32);
            entity.Property(e => e.PressureRatio).HasMaxLength(32);
            entity.Property(e => e.TurbineSpeed).HasMaxLength(32);
        });

        modelBuilder.Entity<TypeOfWork>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TypeOfWork_pkey");

            entity.ToTable("TypeOfWork");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.NameWork).HasMaxLength(55);
        });

        modelBuilder.Entity<UserDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserData_pkey");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Image).HasMaxLength(255);
            entity.Property(e => e.Login).HasMaxLength(32);
            entity.Property(e => e.Name).HasMaxLength(32);
            entity.Property(e => e.Password).HasMaxLength(16);
            entity.Property(e => e.Patronomyc).HasMaxLength(32);
            entity.Property(e => e.Phone).HasMaxLength(12);
            entity.Property(e => e.Surname).HasMaxLength(32);
            entity.Property(e => e.Town);
            entity.Property(e => e.Street);
            entity.Property(e => e.Builder);
            entity.Property(e => e.UserEmail);

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
