using System;

namespace BarkodOtomasyon.Models
{
    public class Barcode
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}