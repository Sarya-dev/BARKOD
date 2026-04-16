namespace BarkodOtomasyon.Services

{
    using BarkodOtomasyon.Data;
    using BarkodOtomasyon.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class BarcodeService
    {
        private readonly DatabaseContext _context;

        public BarcodeService(DatabaseContext context)
        {
            _context = context;
        }

        public void AddBarcode(Barcode barcode)
        {
            _context.Barcodes.Add(barcode);
            _context.SaveChanges();
        }

        public Barcode GetBarcode(int id)
        {
            return _context.Barcodes.FirstOrDefault(b => b.Id == id);
        }

        public List<Barcode> GetAllBarcodes()
        {
            return _context.Barcodes.ToList();
        }

        public void UpdateBarcode(Barcode barcode)
        {
            _context.Barcodes.Update(barcode);
            _context.SaveChanges();
        }

        public void DeleteBarcode(int id)
        {
            var barcode = GetBarcode(id);
            if (barcode != null)
            {
                _context.Barcodes.Remove(barcode);
                _context.SaveChanges();
            }
        }
    }
}