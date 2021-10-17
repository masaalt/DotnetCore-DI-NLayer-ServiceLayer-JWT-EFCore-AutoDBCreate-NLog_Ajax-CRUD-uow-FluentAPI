using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DB.Core.Models
{
    public partial class MVCEFCoreContext : DbContext
    {
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Users> UserModel { get; set; }
        public virtual DbSet<UserDTO> UserDTO { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"data source=.;initial catalog=MyNewDb;integrated security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmpId);

                entity.Property(e => e.EmpName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EmpFamilyName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EmpFatherName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NationalCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.mobile)
                    .HasMaxLength(13)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserDTO>(entity =>
            {
                entity.HasKey(e => e.id);

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
