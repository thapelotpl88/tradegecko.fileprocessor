using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace tradegecko.fileprocessor.Domain.Entities
{
    public partial class TradegeckoDbContext : DbContext
    {
        public TradegeckoDbContext()
        {
        }

        public TradegeckoDbContext(DbContextOptions<TradegeckoDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ObjectTransaction> ObjectTransaction { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=Tradegecko");
			}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<ObjectTransaction>(entity =>
            {
                entity.HasKey(e => e.TransactionId);

                entity.ToTable("object_transaction");

                entity.Property(e => e.TransactionId).HasColumnName("transaction_id");

                entity.Property(e => e.ObjectChanges)
                    .IsRequired()
                    .HasColumnName("object_changes");

                entity.Property(e => e.ObjectId).HasColumnName("object_id");

                entity.Property(e => e.ObjectType)
                    .IsRequired()
                    .HasColumnName("object_type")
                    .HasMaxLength(50);

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .HasColumnName("timestamp")
                    .IsRowVersion();
            });
        }
    }
}
