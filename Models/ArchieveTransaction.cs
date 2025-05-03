namespace InventorySystem.Models
{
    public class ArchieveTransaction
    {
        public int Id { get; set; }

        public TransactionType TransactionType { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int? FromWarehouseId { get; set; }
        public Warehouse? FromWarehouse { get; set; }

        public int? ToWarehouseId { get; set; }
        public Warehouse? ToWarehouse { get; set; }

        public int Quantity { get; set; }

        public DateTime Date { get; set; }

        public string? UserName { get; set; }
        public DateTime ArcheiveData { get; set; }
    }
}
