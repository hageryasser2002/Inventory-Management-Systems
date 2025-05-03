using Microsoft.IdentityModel.Tokens;
using InventorySystem.Data;
using InventorySystem.DTOs.Reports;
using InventorySystem.Models;
using InventorySystem.Models.Reports;
using InventorySystem.Repositories;

namespace InventorySystem.Service
{
    public class ReportService:IReportService
    {
        private readonly AppDbContext _context;

        public ReportService(AppDbContext context)
        {
            _context = context;
        }

        public List<LowStockProductDto> GenerateLowStockReport()
        {
            var lowStockProducts = _context.products
                .Where(p => p.Quantity < p.LowStockThreshold)
                .Select(p => new LowStockProductDto
                {
                    Id = p.Id,
                    ProductName = p.Name,
                    Quantity = p.Quantity,
                    LowStockThreshold = p.LowStockThreshold
                })
                .ToList();

            return lowStockProducts;
        }

        public List<TransactionHistoryDto> GetTransactionHistory(DateTime? fromDate = null, DateTime? toDate = null, string? productName = null, string? transactionType = null)
        {
            var query = _context.InventoryTransactions.AsQueryable();

            if (fromDate.HasValue)
                query = query.Where(t => t.Date >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(t => t.Date <= toDate.Value);

            if (!string.IsNullOrEmpty(transactionType) && Enum.TryParse(transactionType, true, out TransactionType parsedType))
            {
                query = query.Where(t => t.TransactionType == parsedType);
            }

            return query
                .Join(_context.products,
                    t => t.ProductId,
                    p => p.Id,
                    (t, p) => new TransactionHistoryDto
                    {
                        Id = t.Id,
                        ProductName = p.Name,
                        TransactionType = t.TransactionType,
                        Quantity = t.Quantity,
                        Date = t.Date,
                        UserName = t.UserName
                    })
                .OrderByDescending(t => t.Date)
                .ToList();
        }

       
    }
}
