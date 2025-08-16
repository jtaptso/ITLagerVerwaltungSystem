using System.Collections.Generic;

namespace ITLagerVerwaltungSystem.Core.DTOs
{
    public class OrderRequestDto
    {
        public int UserId { get; set; }
        public List<int> MaterialIds { get; set; } = new();
    }
}
