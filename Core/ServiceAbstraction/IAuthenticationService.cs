
using Shared.DataTransferedObjects.IdentityDTOs;

namespace ServiceAbstraction
{
    public interface IAuthenticationService
    {
        // Login
        Task<UserDTO> LoginAsync(LoginDTO loginDTO);

        // Register
        Task<UserDTO> RegisterAsync(RegisterDTO registerDTO);

        // check email
        // take (string)email and then return bool
        Task<bool> CheckEmailAsync(string Email);


        // get current user's address
        // take (string)email then return current logged in user (AddressDTO)address
        Task<AddressDTO> GetCurrentaUserAddresAsync(string Email);


        // update current logged in user address
        // take updated (AddressDTO)address and (string)email then return (AddressDTO)address after udpate
        Task<AddressDTO> UpdateCurrentUserAddressAsync(string Email, AddressDTO addressDTO);

        // get current logged in user
        // take (string)email then return UserDTO(token, email, and display name)
        Task<UserDTO> GetCurrentUserAsync(string Email);
    }
}
