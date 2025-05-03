using InventorySystem.Models;

namespace InventorySystem.Repositories
{
    public interface IInventoryTransactionRepository:IGenericRepository<InventoryTransaction>
    {
        void AddTransaction(InventoryTransaction transaction);
        List<InventoryTransaction> GetTransactions();
        List<InventoryTransaction> GetByProduct(int productId);
        void Save();
    }
}
