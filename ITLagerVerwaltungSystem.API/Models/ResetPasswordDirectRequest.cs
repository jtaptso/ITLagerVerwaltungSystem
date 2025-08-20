namespace ITLagerVerwaltungSystem.API.Models
{
    public class ResetPasswordDirectRequest
    {
        public string Email { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
