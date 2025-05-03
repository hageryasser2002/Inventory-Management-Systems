using InventorySystem.DTOs;
using InventorySystem.Models;

namespace InventorySystem.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        void Add(ProductDTO obj);
    }
}
