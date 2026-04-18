namespace StokTakipSistemi.Services
{
    using StokTakipSistemi.Data;
    using StokTakipSistemi.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class DepotService
    {
        private readonly DatabaseContext _context;

        public DepotService(DatabaseContext context)
        {
            _context = context;
        }

        public void AddDepot(Depot depot)
        {
            _context.Depots.Add(depot);
            _context.SaveChanges();
        }

        public List<Depot> GetAllDepots()
        {
            return _context.Depots.ToList();
        }

        public Depot? GetDepotById(int id)
        {
            return _context.Depots.FirstOrDefault(d => d.Id == id);
        }

        public Depot? GetDepotByName(string name)
        {
            return _context.Depots.FirstOrDefault(d => d.Name == name);
        }

        public void UpdateDepot(Depot depot)
        {
            _context.Depots.Update(depot);
            _context.SaveChanges();
        }

        public void DeleteDepot(int id)
        {
            var depot = GetDepotById(id);
            if (depot != null)
            {
                _context.Depots.Remove(depot);
                _context.SaveChanges();
            }
        }
    }
}
