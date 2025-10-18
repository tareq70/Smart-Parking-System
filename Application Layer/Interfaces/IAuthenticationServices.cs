using Smart_Parking_System.Application_Layer.Dtos.Auth;

namespace Smart_Parking_System.Application_Layer.Interfaces
{
    public interface IAuthenticationServices
    {
        Task<string> RegisterUserAsync(RegisterDTO register);
        Task<string> LoginUserAsync(LoginDTO login);

    }
}
