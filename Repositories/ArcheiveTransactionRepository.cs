
using InventorySystem.Data;
using InventorySystem.Models;

namespace InventorySystem.Repositories
{
    public class ArcheiveTransactionRepository : IArcheiveTransactionRepository
    {
        private readonly AppDbContext _context;

        public ArcheiveTransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(ArchieveTransaction  transaction)
        {
            _context.archieveTransactions.Add(transaction);
            Save();
        }

        public void Delete(int id)
        {
            var transaction = _context.archieveTransactions.FirstOrDefault(p => p.Id == id);
            if (transaction != null)
            {
                _context.archieveTransactions.Remove(transaction);
                Save();
            }
        }

        public List<ArchieveTransaction> GetAll()
        {
            return _context.archieveTransactions.ToList();
        }

        public ArchieveTransaction GetById(int id)
        {
            return _context.archieveTransactions
                           .FirstOrDefault(p => p.Id == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(ArchieveTransaction obj)
        {
            _context.archieveTransactions.Update(obj);
            Save();
        }
    }
}
