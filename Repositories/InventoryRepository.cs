using Microsoft.EntityFrameworkCore;
using InventorySystem.Data;
using InventorySystem.DTOs.Reports;
using InventorySystem.Models;

namespace InventorySystem.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<InventoryRepository> _logger;

        public InventoryRepository(AppDbContext context, ILogger<InventoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Add(Inventory inventory)
        {
            _context.inventory.Add(inventory);
            Save();
        }

        public void Delete(int id)
        {
            var item = _context.inventory.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                _context.inventory.Remove(item);
                Save();
            }
        }

        public List<Inventory> GetAll()
        {
            return _context.inventory.ToList();
        }

        public Inventory GetById(int id)
        {
            return _context.inventory.FirstOrDefault(i => i.Id == id);
        }

        public void Update(Inventory obj)
        {
            _context.inventory.Update(obj);
            Save();
        }


        public Inventory GetByProductAndWarehouse(int productId, int warehouseId)
        {
            return _context.inventory
                .FirstOrDefault(i => i.ProductId == productId &&
                                i.WarehouseId == warehouseId);
        }

        public void AddOrUpdateInventory(int productId, int warehouseId, int quantityChange)
        {
            var inventory = GetByProductAndWarehouse(productId, warehouseId);

            if (inventory != null)
            {
                inventory.Quantity += quantityChange;
                Update(inventory);
            }
            else
            {
                var newInventory = new Inventory
                {
                    ProductId = productId,
                    WarehouseId = warehouseId,
                    Quantity = quantityChange
                };
                Add(newInventory);
                inventory = newInventory; 
            }

            var product = _context.products.FirstOrDefault(p => p.Id == productId);
            if (product != null && inventory.Quantity < product.LowStockThreshold)
            {
                _logger.LogWarning("Low stock alert: Product '{ProductName}' has quantity {Quantity}, below threshold {Threshold}",
                    product.Name, inventory.Quantity, product.LowStockThreshold);
            }

        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public List<LowStockProductDto> GetLowStockProducts()
        {
            return _context.inventory
                .Include(i => i.Product)
                .Where(i => i.Quantity < i.Product.LowStockThreshold)
                .Select(i => new LowStockProductDto
                {
                    ProductName = i.Product.Name,
                    Quantity = i.Quantity,
                    LowStockThreshold = i.Product.LowStockThreshold
                }).ToList();
        }

    }
}
