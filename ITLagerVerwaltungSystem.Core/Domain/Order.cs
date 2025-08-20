namespace ITLagerVerwaltungSystem.Core.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        // Removed User navigation property for Identity compatibility
        public DateTime DateRequested { get; set; }
        public string? Status { get; set; }
        public ICollection<Material>? Materials { get; set; }
    }
}
