using System.Collections.Generic;

namespace ITLagerVerwaltungSystem.Core.DTOs
{
    public class OrderResponseDto
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public List<int> MaterialIds { get; set; } = new();
        public string? Status { get; set; }
        public string? Message { get; set; }
    }
}
