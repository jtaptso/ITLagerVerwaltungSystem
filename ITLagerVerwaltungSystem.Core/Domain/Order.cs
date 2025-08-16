namespace ITLagerVerwaltungSystem.Core.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public DateTime DateRequested { get; set; }
        public string? Status { get; set; }
        public ICollection<Material>? Materials { get; set; }
    }
}
