using System;

namespace BarkodOtomasyon.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int BarcodeId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}