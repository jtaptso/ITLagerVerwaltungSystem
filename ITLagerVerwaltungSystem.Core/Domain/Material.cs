namespace ITLagerVerwaltungSystem.Core.Domain
{
    public enum MaterialStatus
    {
        New,
        Used,
        Damaged,
        Retired
    }

    public class Material
    {
        public int Id { get; set; }
        public MaterialType? MaterialType { get; set; }
        public string? Model { get; set; }
        public SerialNumber? SerialNumber { get; set; }
        public Condition? Condition { get; set; }
        public MaterialStatus Status { get; set; }
        public ICollection<MovementLog>? MovementLogs { get; set; }
        public Order? Order { get; set; }

        // Collection of file paths for material pictures
        public ICollection<string>? PicturePaths { get; set; }

        // Quantity of materials in stock
        public int Quantity { get; set; }
    }
}
