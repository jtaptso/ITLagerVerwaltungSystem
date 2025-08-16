namespace ITLagerVerwaltungSystem.Core.Domain
{
    public enum Role
    {
        Manager,
        Employee,
        Gast
    }

    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public Role Role { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Notification>? Notifications { get; set; }
    }
}
