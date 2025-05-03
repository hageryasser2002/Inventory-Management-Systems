using InventorySystem.DTOs.Reports;
using InventorySystem.Models;

namespace InventorySystem.Repositories
{
    public interface IInventoryRepository:IGenericRepository<Inventory>
    {
        Inventory GetByProductAndWarehouse(int productId, int warehouseId);
        void AddOrUpdateInventory(int productId, int warehouseId, int quantityChange);
        List<LowStockProductDto> GetLowStockProducts();

        void Save();
    }
}
