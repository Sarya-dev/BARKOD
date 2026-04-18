namespace StokTakipSistemi.Services
{
    using StokTakipSistemi.Data;
    using StokTakipSistemi.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StockService
    {
        private readonly DatabaseContext _context;

        public StockService(DatabaseContext context)
        {
            _context = context;
        }

        public Product? GetProductByBarcode(string barcodeCode)
        {
            var barcode = _context.Barcodes.FirstOrDefault(b => b.Code == barcodeCode);
            if (barcode == null)
                return null;
            
            return _context.Products.FirstOrDefault(p => p.BarcodeId == barcode.Id);
        }

        public List<Product> GetProductsByDepot(int depotId)
        {
            // Tüm ürünleri al ve bu depoya ait stok hareketleri olanları filtrele
            return _context.Products
                .Where(p => _context.StockMovements.Any(sm => sm.ProductId == p.Id && sm.DepotId == depotId))
                .ToList() ?? new List<Product>();
        }

        public int GetStockByProductAndDepot(int productId, int depotId)
        {
            var movements = _context.StockMovements
                .Where(sm => sm.ProductId == productId && sm.DepotId == depotId)
                .ToList();

            return movements.Sum(m => m.Quantity);
        }

        public void AddStock(int productId, int depotId, int quantity, string notes = "")
        {
            var movement = new StockMovement
            {
                ProductId = productId,
                DepotId = depotId,
                Quantity = quantity,
                MovementType = "IN",
                Notes = notes,
                MovementDate = DateTime.Now
            };

            _context.StockMovements.Add(movement);
            
            var product = _context.Products.Find(productId);
            if (product != null)
            {
                product.Stock += quantity;
                _context.Products.Update(product);
            }

            _context.SaveChanges();
        }

        public void RemoveStock(int productId, int depotId, int quantity, string notes = "")
        {
            var movement = new StockMovement
            {
                ProductId = productId,
                DepotId = depotId,
                Quantity = -quantity,
                MovementType = "OUT",
                Notes = notes,
                MovementDate = DateTime.Now
            };

            _context.StockMovements.Add(movement);
            
            var product = _context.Products.Find(productId);
            if (product != null)
            {
                product.Stock -= quantity;
                _context.Products.Update(product);
            }

            _context.SaveChanges();
        }

        public List<StockMovement> GetStockHistory(int productId)
        {
            return _context.StockMovements
                .Where(sm => sm.ProductId == productId)
                .OrderByDescending(sm => sm.MovementDate)
                .ToList();
        }
    }
}
