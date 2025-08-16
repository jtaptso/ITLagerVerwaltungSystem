namespace ITLagerVerwaltungSystem.Core.DTOs
{
    public class MaterialIssueResponseDto
    {
        public int MaterialId { get; set; }
        public int UserId { get; set; }
        public string? Status { get; set; }
        public string? Message { get; set; }
    }
}
