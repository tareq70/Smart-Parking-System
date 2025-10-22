using Smart_Parking_System.Application_Layer.Dtos.Auth;

namespace Smart_Parking_System.Application_Layer.Interfaces
{
    public interface IAuthenticationServices
    {
        Task<ResponseDTO> RegisterUserAsync(RegisterDTO register);
        Task<ResponseDTO> LoginUserAsync(LoginDTO login);

    }
}
