namespace InventorySystem.Models.Reports
{
    public class TransactionHistoryReport
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public string User { get; set; }


    }
}
