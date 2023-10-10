namespace Inventory.Models
{
    public class LogEntry
    {
        public int Id {get; set;}
        public DateTime Date { get; set; }
        public string Username { get; set; }
        public string ActionCategory { get; set; }
        public string Action { get; set; }
    }
}
