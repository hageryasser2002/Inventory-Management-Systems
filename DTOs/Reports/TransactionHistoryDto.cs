using InventorySystem.Models;

namespace InventorySystem.DTOs.Reports
{
    public class TransactionHistoryDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public TransactionType TransactionType { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
    }
}
