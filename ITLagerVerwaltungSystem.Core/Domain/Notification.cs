namespace ITLagerVerwaltungSystem.Core.Domain
{
    public class Notification
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string? Message { get; set; }
        public DateTime Date { get; set; }
        public bool IsRead { get; set; }
    }
}