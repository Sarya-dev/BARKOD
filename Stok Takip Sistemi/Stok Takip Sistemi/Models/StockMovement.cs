using System;

namespace StokTakipSistemi.Models
{
    public class StockMovement
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int DepotId { get; set; }
        public int Quantity { get; set; } // Pozitif = gelen, Negatif = giden
        public string MovementType { get; set; } = string.Empty; // "IN" veya "OUT"
        public string Notes { get; set; } = string.Empty;
        public DateTime MovementDate { get; set; } = DateTime.Now;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        // İlişkiler
        public virtual Product? Product { get; set; }
        public virtual Depot? Depot { get; set; }
    }
}
