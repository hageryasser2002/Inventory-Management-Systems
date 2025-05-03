using AutoMapper;
using InventorySystem.Data;
using InventorySystem.Models;
using InventorySystem.Service;
using Microsoft.Extensions.Logging;

namespace InventorySystem.BackgroundJobs
{
    public class BackgroundTask
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<BackgroundTask> _logger;

        public BackgroundTask(AppDbContext context, IMapper mapper, ILogger<BackgroundTask> logger, IEmailService emailService)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _emailService = emailService;
        }

        public void DoWork() 
        {
            ArchiveOldTransactions();
            CheckLowStock();
        }
        

        public void ArchiveOldTransactions()
        {
            var oldTransactions = _context.InventoryTransactions
                .Where(t => t.Date < DateTime.Now.AddYears(-1))
                .ToList();

            if (oldTransactions.Any())
            {
                foreach (var transaction in oldTransactions)
                {
                    Console.WriteLine($"Archiving transaction {transaction.Id} dated {transaction.Date}");
                    var transDto = _mapper.Map<ArchieveTransaction>(transaction);
                    _context.archieveTransactions.Add(transDto);
                }
            }
            else
            {
                Console.WriteLine("No old transactions to archive.");
            }
        }
        
        public void CheckLowStock()
        {
            var lowStockProducts = _context.products
                .Where(p => p.Quantity < p.LowStockThreshold)
                .ToList();

            if (lowStockProducts.Any())
            {
                foreach (var product in lowStockProducts)
                {
                    string message = $"Product {product.Name} is low on stock, only {product.Quantity} items remaining.";
                    _logger.LogWarning(message);
                    _emailService.SendEmail("admin@inventory.com", "Low Stock Alert", message);
                }
            }
            else
            {
                Console.WriteLine("No products are low on stock.");
            }
        }
    }
}
