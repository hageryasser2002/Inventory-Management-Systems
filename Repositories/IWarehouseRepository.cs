using InventorySystem.DTOs;
using InventorySystem.Models;

namespace InventorySystem.Repositories
{
    public interface IWarehouseRepository:IGenericRepository<Warehouse>
    {
        void Add(WarehouseDTO warehouse);
    }
}
