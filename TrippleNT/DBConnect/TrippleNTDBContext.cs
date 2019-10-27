using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TrippleNT.DBConnect
{
    public partial class TrippleNTDBContext : DbContext
    {
        public TrippleNTDBContext()
        {
        }

        public TrippleNTDBContext(DbContextOptions<TrippleNTDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<CompanyStaff> CompanyStaff { get; set; }
        public virtual DbSet<Donations> Donations { get; set; }
        public virtual DbSet<Donors> Donors { get; set; }
        public virtual DbSet<Payments> Payments { get; set; }
        public virtual DbSet<Reconciliation> Reconciliation { get; set; }
        public virtual DbSet<SuperAdmin> SuperAdmin { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=stoontdb.ck0xla48kbhe.eu-west-2.rds.amazonaws.com;Database=TrippleNTDB;Trusted_Connection=True; user id=stoontuser password=stoont2019");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Rcno)
                    .HasColumnName("RCNo")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RegDate).HasColumnType("datetime");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CompanyStaff>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyStaff)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyStaff_Company");
            });

            modelBuilder.Entity<Donations>(entity =>
            {
                entity.HasKey(e => e.DonationId);

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.DateDonated).HasColumnType("datetime");

                entity.Property(e => e.Donor)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.HasOne(d => d.Comapany)
                    .WithMany(p => p.Donations)
                    .HasForeignKey(d => d.ComapanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Donations_Company");

                entity.HasOne(d => d.CompanyStaff)
                    .WithMany(p => p.Donations)
                    .HasForeignKey(d => d.CompanyStaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Donations_CompanyStaff");
            });

            modelBuilder.Entity<Donors>(entity =>
            {
                entity.HasKey(e => e.PhoneNumber);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Payments>(entity =>
            {
                entity.HasKey(e => e.PaymentId);

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.DateOfPayment).HasColumnType("datetime");

                entity.Property(e => e.Reference)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payments_Company");

                entity.HasOne(d => d.Reconcile)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.ReconcileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payments_Reconciliation");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payments_CompanyStaff");
            });

            modelBuilder.Entity<Reconciliation>(entity =>
            {
                entity.HasKey(e => e.ReconcileId);

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.EndPeriod).HasColumnType("date");

                entity.Property(e => e.StartPeriod).HasColumnType("date");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Reconciliation)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reconciliation_Company");
            });

            modelBuilder.Entity<SuperAdmin>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });
        }
    }
}
