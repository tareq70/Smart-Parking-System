using Microsoft.AspNetCore.Identity;

namespace Smart_Parking_System.Domain_Layer.Entities
{
    public class ApplicationUser : IdentityUser
    {

        public string FullName { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public string SSN { get; set; } = string.Empty;



    }
}
