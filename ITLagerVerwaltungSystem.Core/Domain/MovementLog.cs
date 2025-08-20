namespace ITLagerVerwaltungSystem.Core.Domain
{
    public enum MovementType
    {
        Procurement,
        Issue,
        Return,
        Reissue,
        Damage
    }

    public class MovementLog
    {
        public int Id { get; set; }
        public int MaterialId { get; set; }
        public Material? Material { get; set; }
        public MovementType MovementType { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; } = string.Empty;
        // Removed User navigation property for Identity compatibility
    }
}
