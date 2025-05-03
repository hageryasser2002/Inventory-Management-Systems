namespace InventorySystem.Models.Reports
{
    public class LowStockReport
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int LowStockThreshold { get; set; }
        public string Category { get; set; }


    }
}
