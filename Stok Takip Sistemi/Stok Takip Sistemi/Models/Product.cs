using System;
using System.Collections.Generic;

namespace StokTakipSistemi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int BarcodeId { get; set; }
        public string Season { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Series { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        public virtual ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
    }
}
