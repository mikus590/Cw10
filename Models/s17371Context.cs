using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Cw3.ORMModels
{
    public partial class s17371Context : DbContext
    {
        public s17371Context()
        {
        }

        public s17371Context(DbContextOptions<s17371Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Enrollment> Enrollment { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Studies> Studies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=db-mssql;Initial Catalog=s17371;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => e.IdEnrollment);

                entity.Property(e => e.IdEnrollment).ValueGeneratedNever();

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.IdStudyNavigation)
                    .WithMany(p => p.Enrollment)
                    .HasForeignKey(d => d.IdStudy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Enrollment_Studies");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.IndexNumber);

                entity.Property(e => e.IndexNumber)
                    .HasMaxLength(100)
                    .ValueGeneratedNever();

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Pswd)
                    .HasColumnName("pswd")
                    .HasMaxLength(100);

                entity.Property(e => e.RefreshToken)
                    .HasColumnName("refreshToken")
                    .HasMaxLength(100);

                entity.Property(e => e.Salt)
                    .HasColumnName("salt")
                    .HasMaxLength(32);

                entity.HasOne(d => d.IdEnrollmentNavigation)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.IdEnrollment)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Student_Enrollment");
            });

            modelBuilder.Entity<Studies>(entity =>
            {
                entity.HasKey(e => e.IdStudy);

                entity.Property(e => e.IdStudy).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        }
    }
}
