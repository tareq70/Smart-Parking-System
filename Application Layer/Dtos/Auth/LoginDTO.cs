using System.ComponentModel.DataAnnotations;

namespace Smart_Parking_System.Application_Layer.Dtos.Auth
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
