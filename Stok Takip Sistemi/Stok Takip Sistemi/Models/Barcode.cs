namespace StokTakipSistemi.Models
{
    public class Barcode
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
