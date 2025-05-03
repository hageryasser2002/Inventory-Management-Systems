namespace InventorySystem.DTOs.Reports
{
    public class LowStockProductDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int LowStockThreshold { get; set; }
    }
}
