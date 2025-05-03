using InventorySystem.Data;
using InventorySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Repositories
{
    public class InventoryTransactionRepository : IInventoryTransactionRepository
    {
        private readonly AppDbContext _context;

        public InventoryTransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(InventoryTransaction  inventoryTransaction)
        {
            _context.InventoryTransactions.Add(inventoryTransaction);
            Save();
        }

        public void AddTransaction(InventoryTransaction transaction)
        {
            _context.InventoryTransactions.Add(transaction);
            Save();
        }

        public void Delete(int id)
        {
            var transaction = _context.InventoryTransactions.Find(id);
            if (transaction != null)
            {
                _context.InventoryTransactions.Remove(transaction);
                Save();
            }
        }

        public List<InventoryTransaction> GetAll()
        {
            return _context.InventoryTransactions
                .Include(t => t.Product)
                .Include(t => t.FromWarehouse)
                .Include(t => t.ToWarehouse)
                .ToList();
        }

        public InventoryTransaction GetById(int id)
        {
            return _context.InventoryTransactions
                .Include(t => t.Product)
                .Include(t => t.FromWarehouse)
                .Include(t => t.ToWarehouse)
                .FirstOrDefault(t => t.Id == id);
        }

        public List<InventoryTransaction> GetByProduct(int productId)
        {
            return _context.InventoryTransactions
                .Where(t => t.ProductId == productId)
                .Include(t => t.FromWarehouse)
                .Include(t => t.ToWarehouse)
                .ToList();
        }

        public List<InventoryTransaction> GetTransactions()
        {
            return _context.InventoryTransactions.ToList();
        }

        public void Update(InventoryTransaction obj)
        {
            _context.InventoryTransactions.Update(obj);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
