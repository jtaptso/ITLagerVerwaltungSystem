
using System.ComponentModel.DataAnnotations;

namespace ITLagerVerwaltungSystem.Core.DTOs
{
    public class UserRegisterDto
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [MinLength(6)]
        public string? Password { get; set; }

        // Standard role is Employee. Role property is not needed for registration.
    }
}
