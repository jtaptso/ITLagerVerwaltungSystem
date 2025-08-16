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
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
