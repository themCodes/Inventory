namespace Inventory.Models
{
    public class InventoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public int NumberOfItems { get; set; }
        public int NumberOfItemsTrigger { get; set; }
        public string Location { get; set; }
        public string Comment { get; set; }
        public DateTime LastInventoryCount { get; set; }
    }
}
