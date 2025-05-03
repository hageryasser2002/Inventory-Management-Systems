namespace InventorySystem.Models
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Manager { get; set; }

        public Inventory inventory { get; set; }

    }
}
