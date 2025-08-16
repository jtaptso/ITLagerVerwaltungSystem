using System.ComponentModel.DataAnnotations;

namespace ITLagerVerwaltungSystem.API.Models
{
    public class LoginModel
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
