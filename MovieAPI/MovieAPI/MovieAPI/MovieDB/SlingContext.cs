using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MovieAPI.MovieDB
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

        public virtual DbSet<Movie> Movies { get; set; } = null!;

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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
