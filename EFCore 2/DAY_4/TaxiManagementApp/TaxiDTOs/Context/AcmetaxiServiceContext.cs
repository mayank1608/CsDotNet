using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using TaxiDTOs.Models;

namespace TaxiDTOs.Context;

public partial class AcmetaxiServiceContext : DbContext
{
    private readonly string _connectionString;
    public AcmetaxiServiceContext()
    {
        _connectionString = ConfigurationManager.ConnectionStrings["TaxiDB"].ConnectionString;
    }

    public AcmetaxiServiceContext(DbContextOptions<AcmetaxiServiceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AllocateTaxi> AllocateTaxis { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Cab> Cabs { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(_connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AllocateTaxi>(entity =>
        {
            entity.HasKey(e => e.MapId).HasName("PK__Allocate__3265E2FBD24D0F74");

            entity.ToTable("AllocateTaxi");

            entity.Property(e => e.MapId).HasColumnName("MapID");
            entity.Property(e => e.CabId).HasColumnName("CabID");
            entity.Property(e => e.DriverId)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Cab).WithMany(p => p.AllocateTaxis)
                .HasForeignKey(d => d.CabId)
                .HasConstraintName("FK__AllocateT__CabID__4F7CD00D");

            entity.HasOne(d => d.Driver).WithMany(p => p.AllocateTaxis)
                .HasForeignKey(d => d.DriverId)
                .HasConstraintName("FK__AllocateT__Drive__4E88ABD4");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Booking__73951ACD286BC107");

            entity.ToTable("Booking");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.BookingDate).HasColumnType("datetime");
            entity.Property(e => e.Destination)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Fare).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MapId).HasColumnName("MapID");
            entity.Property(e => e.Source)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Map).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.MapId)
                .HasConstraintName("FK__Booking__MapID__52593CB8");
        });

        modelBuilder.Entity<Cab>(entity =>
        {
            entity.HasKey(e => e.CabId).HasName("PK__Cab__66AC414DC7159A87");

            entity.ToTable("Cab");

            entity.Property(e => e.CabId)
                .ValueGeneratedNever()
                .HasColumnName("CabID");
            entity.Property(e => e.Carnumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ManufactureYear).HasColumnName("Manufacture_year");
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.DriverId).HasName("PK__Driver__F1B1CD0468711FDB");

            entity.ToTable("Driver");

            entity.HasIndex(e => e.DrivingLicenceNo, "UQ__Driver__2868303884DBCD02").IsUnique();

            entity.Property(e => e.DriverId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DrivingLicenceNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Driving_licence_no");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("First_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Last_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
