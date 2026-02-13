
using Shared.DataTransferedObjects.IdentityDTOs;

namespace ServiceAbstraction
{
    public interface IAuthenticationService
    {
        // Login
        Task<UserDTO> LoginAsync(LoginDTO loginDTO);

        // Register
        Task<UserDTO> RegisterAsync(RegisterDTO registerDTO);
    }
}
