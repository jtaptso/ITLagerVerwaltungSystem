
namespace ITLagerVerwaltungSystem.Core.Domain
{
    public class Notification
    {
        public Notification() { }

        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public string? Message { get; set; }
        public DateTime Date { get; set; }
        public bool IsRead { get; set; }
    }
}
