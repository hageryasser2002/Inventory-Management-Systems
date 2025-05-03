using InventorySystem.Data;
using InventorySystem.DTOs;
using InventorySystem.Models;

namespace InventorySystem.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly AppDbContext _context;

        public WarehouseRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(WarehouseDTO warehouse)
        {
            var warehouseObj = new Warehouse()
            {
                Name = warehouse.Name,
                Location = warehouse.Location,
                Manager = warehouse.Manager
            };
            _context.warehouses.Add(warehouseObj);
            Save();
        }

        public void Delete(int id)
        {
           Warehouse warehouse= _context.warehouses.Where(w=>w.Id==id).FirstOrDefault();
            if(warehouse!=null)
            {
               _context.warehouses.Remove(warehouse);
            }
            Save();
        }

        public List<Warehouse> GetAll()
        {
            return _context.warehouses.ToList();
        }

        public Warehouse GetById(int id)
        {
            return _context.warehouses.FirstOrDefault(w => w.Id == id);
        }

        public void Update(Warehouse warehouse)
        {
            _context.warehouses.Update(warehouse);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Add(Warehouse obj)
        {
            _context.warehouses.Add(obj);
            Save();
        }

       
    }
}
