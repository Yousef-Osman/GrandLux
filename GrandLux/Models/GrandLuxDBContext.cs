using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GrandLux.Models
{
    public partial class GrandLuxDBContext : DbContext
    {
        public GrandLuxDBContext()
        {
        }

        public GrandLuxDBContext(DbContextOptions<GrandLuxDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Guests> Guests { get; set; }
        public virtual DbSet<ReservationStatus> ReservationStatus { get; set; }
        public virtual DbSet<Reservations> Reservations { get; set; }
        public virtual DbSet<RoomStatus> RoomStatus { get; set; }
        public virtual DbSet<RoomType> RoomType { get; set; }
        public virtual DbSet<Rooms> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasIndex(e => e.EMail)
                    .HasName("IX_Employees")
                    .IsUnique();

                entity.HasIndex(e => e.Phone)
                    .HasName("IX_Employees_1")
                    .IsUnique();

                entity.Property(e => e.EMail)
                    .HasColumnName("E_Mail")
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("First_Name")
                    .HasMaxLength(50);

                entity.Property(e => e.JobTitle)
                    .IsRequired()
                    .HasColumnName("Job_Title")
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("Last_Name")
                    .HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Guests>(entity =>
            {
                entity.HasIndex(e => e.EMail)
                    .HasName("IX_Guests")
                    .IsUnique();

                entity.HasIndex(e => e.Phone)
                    .HasName("IX_Guests_1")
                    .IsUnique();

                entity.Property(e => e.EMail)
                    .HasColumnName("E_Mail")
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("First_Name")
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("Last_Name")
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ReservationStatus>(entity =>
            {
                entity.ToTable("Reservation_Status");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Reservations>(entity =>
            {
                entity.Property(e => e.CheckIn)
                    .HasColumnName("Check_In")
                    .HasColumnType("datetime");

                entity.Property(e => e.CheckOut)
                    .HasColumnName("Check_Out")
                    .HasColumnType("datetime");

                entity.Property(e => e.GuestId).HasColumnName("Guest_Id");

                entity.Property(e => e.ReservationStatus).HasColumnName("Reservation_Status");

                entity.Property(e => e.RoomId).HasColumnName("Room_Id");

                entity.HasOne(d => d.Guest)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.GuestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservations_Guests");

                entity.HasOne(d => d.ReservationStatusNavigation)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.ReservationStatus)
                    .HasConstraintName("FK_Reservations_Reservation_Status");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservations_Rooms");
            });

            modelBuilder.Entity<RoomStatus>(entity =>
            {
                entity.ToTable("Room_Status");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<RoomType>(entity =>
            {
                entity.ToTable("Room_Type");

                entity.Property(e => e.MaxCapacity).HasColumnName("Max_Capacity");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NoOfBeds).HasColumnName("No_of_Beds");
            });

            modelBuilder.Entity<Rooms>(entity =>
            {
                entity.HasIndex(e => e.RoomNumber)
                    .HasName("IX_Rooms")
                    .IsUnique();

                entity.Property(e => e.CheckIn)
                    .HasColumnName("Check_In")
                    .HasColumnType("datetime");

                entity.Property(e => e.CheckOut)
                    .HasColumnName("Check_Out")
                    .HasColumnType("datetime");

                entity.Property(e => e.FloorNumber).HasColumnName("Floor_Number");

                entity.Property(e => e.RoomNumber).HasColumnName("Room_Number");

                entity.Property(e => e.StatusId).HasColumnName("Status_Id");

                entity.Property(e => e.TypeId).HasColumnName("Type_Id");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rooms_Room_Status");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rooms_Room_Type");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
