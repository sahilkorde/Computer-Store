using Computer_Store_Models;

namespace Computer_Store_ClientStore.Service.IService
{
    public interface IAuthenticationService
    {
        Task<SignUpResponseDTO> RegisterUser(SignUpRequestDTO signUpRequest);
        Task<SignInresponseDTO> Login(SignInRequestDTO signInRequest);
        Task Logout();
    }
}
