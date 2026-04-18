using System;
using System.Collections.Generic;

namespace StokTakipSistemi.Models
{
    public class Depot
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        // İlişkiler
        public virtual ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();

        public override string ToString()
        {
            return Name;
        }
    }
}
