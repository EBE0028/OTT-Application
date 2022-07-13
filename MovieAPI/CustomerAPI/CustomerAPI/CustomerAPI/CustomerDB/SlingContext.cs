using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CustomerAPI.CustomerDB
{
    public partial class SlingContext : DbContext
    {
        public SlingContext()
        {
        }

        public SlingContext(DbContextOptions<SlingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Movie> Movies { get; set; } = null!;
        public virtual DbSet<SubscriptionPlan> SubscriptionPlans { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=Sling;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustId)
                    .HasName("PK__Customer__049E3A89A6AA4A12");

                entity.ToTable("Customer");

                entity.Property(e => e.CustId).HasColumnName("CustID");

                entity.Property(e => e.CustEmail)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.CustName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustPassword)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PayDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("Movie");

                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.MovieCategory)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Action')");

                entity.Property(e => e.MovieContent).IsUnicode(false);

                entity.Property(e => e.MovieImage).IsUnicode(false);

                entity.Property(e => e.MovieLanguage)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('English')");

                entity.Property(e => e.MovieName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MovieRating).HasDefaultValueSql("((3))");
            });

            modelBuilder.Entity<SubscriptionPlan>(entity =>
            {
                entity.HasKey(e => e.PlanId)
                    .HasName("PK__Subscrip__755C22D7E85F724B");

                entity.ToTable("SubscriptionPlan");

                entity.Property(e => e.PlanId).HasColumnName("PlanID");

                entity.Property(e => e.PlanName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
