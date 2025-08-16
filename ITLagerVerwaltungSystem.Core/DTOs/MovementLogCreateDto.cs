using System;
using System.ComponentModel.DataAnnotations;

namespace ITLagerVerwaltungSystem.Core.DTOs
{
    public class MovementLogCreateDto
    {
        [Required]
        public int MaterialId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string MovementType { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; }
    }
}
