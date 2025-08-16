using System.ComponentModel.DataAnnotations;

namespace ITLagerVerwaltungSystem.API.Models
{
    public class RegisterModel
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Role { get; set; }
    }
}
