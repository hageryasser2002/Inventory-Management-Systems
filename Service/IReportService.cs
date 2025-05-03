using InventorySystem.DTOs.Reports;
using InventorySystem.Models.Reports;

namespace InventorySystem.Service
{
    public interface IReportService
    {
        List<LowStockProductDto> GenerateLowStockReport();

        List<TransactionHistoryDto> GetTransactionHistory(
            DateTime? fromDate = null,
            DateTime? toDate = null,
            string? productName = null,
            string? transactionType = null
        );
    }
}
