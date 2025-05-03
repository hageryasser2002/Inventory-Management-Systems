namespace InventorySystem.Models.Reports
{
    public class ReportFilters
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ProductCategory { get; set; }
        public string TransactionType { get; set; }

    }
}
