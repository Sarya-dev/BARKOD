using Microsoft.EntityFrameworkCore;
using BarkodOtomasyon.Models;

namespace BarkodOtomasyon.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Barcode> Barcodes { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Barcode unique constraint
            modelBuilder.Entity<Barcode>()
                .HasIndex(b => b.Code)
                .IsUnique();

            // Price precision
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);
        }
    }
}