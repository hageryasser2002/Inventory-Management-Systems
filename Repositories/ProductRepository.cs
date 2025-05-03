using InventorySystem.Data;
using InventorySystem.DTOs;
using InventorySystem.Models;

namespace InventorySystem.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(ProductDTO obj)
        {
            var product = new Product()
            {
                Name = obj.Name,
                Description = obj.Description,
                Category= obj.Category,
                Price = obj.Price,
                LowStockThreshold = obj.LowStockThreshold,
                Quantity = obj.Quantity
            };
            _context.products.Add(product);
            Save();
        }

        public void Delete(int id)
        {
            var product = _context.products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _context.products.Remove(product);
                Save();
            }
        }

        public List<Product> GetAll()
        {
            return _context.products.ToList();
        }

        public Product GetById(int id)
        {
            return _context.products.FirstOrDefault(p => p.Id == id);
        }

        public void Update(Product obj)
        {
            _context.products.Update(obj);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Add(Product obj)
        {
            _context.products.Add(obj);
            Save();
        }

    }
}
