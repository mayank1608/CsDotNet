using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DBFirstDTO.RajatModels;

public partial class CertificationTrackerDbContext : DbContext
{
    public CertificationTrackerDbContext()
    {
    }

    public CertificationTrackerDbContext(DbContextOptions<CertificationTrackerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblBusinessUnit> TblBusinessUnits { get; set; }

    public virtual DbSet<TblCertification> TblCertifications { get; set; }

    public virtual DbSet<TblCertificationLevel> TblCertificationLevels { get; set; }

    public virtual DbSet<TblCertificationProvider> TblCertificationProviders { get; set; }

    public virtual DbSet<TblCountry> TblCountries { get; set; }

    public virtual DbSet<TblCstatus> TblCstatuses { get; set; }

    public virtual DbSet<TblEmployee> TblEmployees { get; set; }

    public virtual DbSet<TblEmployeeCertification> TblEmployeeCertifications { get; set; }

    public virtual DbSet<TblLocation> TblLocations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=CertificationTrackerDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblBusinessUnit>(entity =>
        {
            entity.HasKey(e => e.UnitCode).HasName("PK__tbl_Busi__0665E6D8355DF36E");

            entity.ToTable("tbl_BusinessUnit");

            entity.Property(e => e.UnitCode)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Unit)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblCertification>(entity =>
        {
            entity.HasKey(e => e.CertificationId).HasName("PK__tbl_CERT__1237E5AACF9E179A");

            entity.ToTable("tbl_CERTIFICATIONS");

            entity.Property(e => e.CertificationId).HasColumnName("CertificationID");
            entity.Property(e => e.CertificationProvider)
                .HasMaxLength(4)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasOne(d => d.CertificationLevelNavigation).WithMany(p => p.TblCertifications)
                .HasForeignKey(d => d.CertificationLevel)
                .HasConstraintName("FK_Cert_Level");

            entity.HasOne(d => d.CertificationProviderNavigation).WithMany(p => p.TblCertifications)
                .HasForeignKey(d => d.CertificationProvider)
                .HasConstraintName("FK_Cert_Provider");
        });

        modelBuilder.Entity<TblCertificationLevel>(entity =>
        {
            entity.HasKey(e => e.ClevelId).HasName("PK__tbl_Cert__238BAA63D76F5E72");

            entity.ToTable("tbl_CertificationLevel");

            entity.Property(e => e.ClevelId).HasColumnName("CLevelID");
            entity.Property(e => e.Clevel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CLevel");
        });

        modelBuilder.Entity<TblCertificationProvider>(entity =>
        {
            entity.HasKey(e => e.CproviderId).HasName("PK__tbl_Cert__36F9C6D7A05E6F28");

            entity.ToTable("tbl_CertificationProvider");

            entity.Property(e => e.CproviderId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("CProviderID");
            entity.Property(e => e.Cprovider)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CProvider");
        });

        modelBuilder.Entity<TblCountry>(entity =>
        {
            entity.HasKey(e => e.CountryCode).HasName("PK__tbl_Coun__5D9B0D2D36D83C78");

            entity.ToTable("tbl_Country");

            entity.Property(e => e.CountryCode).ValueGeneratedNever();
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblCstatus>(entity =>
        {
            entity.HasKey(e => e.CstatusId).HasName("PK__tbl_CSta__6C34656C5DEFCABA");

            entity.ToTable("tbl_CStatus");

            entity.Property(e => e.CstatusId).HasColumnName("CStatusID");
            entity.Property(e => e.Cstatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CStatus");
        });

        modelBuilder.Entity<TblEmployee>(entity =>
        {
            entity.HasKey(e => e.Psnumber).HasName("PK__tbl_Empl__596A1D908BA0595B");

            entity.ToTable("tbl_Employee");

            entity.Property(e => e.Psnumber)
                .ValueGeneratedNever()
                .HasColumnName("PSNumber");
            entity.Property(e => e.BusinessUnit)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.EmailId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EmailID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.BusinessUnitNavigation).WithMany(p => p.TblEmployees)
                .HasForeignKey(d => d.BusinessUnit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_BusinessUnit");

            entity.HasOne(d => d.EmpLocationNavigation).WithMany(p => p.TblEmployees)
                .HasForeignKey(d => d.EmpLocation)
                .HasConstraintName("FK_Employee_Location");
        });

        modelBuilder.Entity<TblEmployeeCertification>(entity =>
        {
            entity.HasKey(e => e.RegistrationId).HasName("PK__tbl_Empl__6EF5883014493FA1");

            entity.ToTable("tbl_EmployeeCertifications");

            entity.Property(e => e.RegistrationId).HasColumnName("RegistrationID");
            entity.Property(e => e.CertificationId).HasColumnName("CertificationID");
            entity.Property(e => e.EmployeePsnumber).HasColumnName("EmployeePSNumber");

            entity.HasOne(d => d.Certification).WithMany(p => p.TblEmployeeCertifications)
                .HasForeignKey(d => d.CertificationId)
                .HasConstraintName("FK_EmpCert_Certification");

            entity.HasOne(d => d.CertificationStatusNavigation).WithMany(p => p.TblEmployeeCertifications)
                .HasForeignKey(d => d.CertificationStatus)
                .HasConstraintName("FK_EmpCert_Status");

            entity.HasOne(d => d.EmployeePsnumberNavigation).WithMany(p => p.TblEmployeeCertifications)
                .HasForeignKey(d => d.EmployeePsnumber)
                .HasConstraintName("FK_EmpCert_Employee");
        });

        modelBuilder.Entity<TblLocation>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK__tbl_Loca__E7FEA477A74BFE5A");

            entity.ToTable("tbl_Locations");

            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.Lname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LName");

            entity.HasOne(d => d.CountryNavigation).WithMany(p => p.TblLocations)
                .HasForeignKey(d => d.Country)
                .HasConstraintName("FK_Locations_Country");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
