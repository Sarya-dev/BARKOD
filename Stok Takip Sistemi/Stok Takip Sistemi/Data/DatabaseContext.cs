using Microsoft.EntityFrameworkCore;
using StokTakipSistemi.Models;

namespace StokTakipSistemi.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Barcode> Barcodes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Depot> Depots { get; set; }
        public DbSet<StockMovement> StockMovements { get; set; }

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

            // StockMovement ilişkileri
            modelBuilder.Entity<StockMovement>()
                .HasOne(sm => sm.Product)
                .WithMany(p => p.StockMovements)
                .HasForeignKey(sm => sm.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StockMovement>()
                .HasOne(sm => sm.Depot)
                .WithMany(d => d.StockMovements)
                .HasForeignKey(sm => sm.DepotId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
