using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ASI.Basecode.Data.Models;

namespace ASI.Basecode.Data
{
    public partial class AsiBasecodeDBContext : DbContext
    {
        public AsiBasecodeDBContext()
        {
        }

        public AsiBasecodeDBContext(DbContextOptions<AsiBasecodeDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Expense> Expenses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Username, "UQ__Users__1788CC4D5F4A160F")
                    .IsUnique();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<Expense>(entity =>
            {
                entity.ToTable("Expense");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.ExpenseTitle)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CategoryId)
                    .IsRequired();

                entity.HasOne(e => e.Category)
                    .WithMany(c => c.Expenses)
                    .HasForeignKey(e => e.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.User)
                    .WithMany(u => u.Expenses)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(10, 2)")
                    .IsRequired();

                entity.Property(e => e.DateCreated)
                    .HasDefaultValueSql("GETDATE()");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.DateCreated)
                    .HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.User)
                    .WithMany(u => u.Categories)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
