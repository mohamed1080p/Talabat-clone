
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferedObjects.IdentityDTOs;
using System.Security.Claims;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManager _serviceManager):ApiBaseController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user= await _serviceManager.AuthenticationService.LoginAsync(loginDTO);
            return Ok(user);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            var user = await _serviceManager.AuthenticationService.RegisterAsync(registerDTO);
            return Ok(user);
        }
        // check email

        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string Email)
        {
            var result = await _serviceManager.AuthenticationService.CheckEmailAsync(Email);
            return Ok(result);
        }

        // get current user
        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var AppUser = await _serviceManager.AuthenticationService.GetCurrentUserAsync(email!);
            return Ok(AppUser);
        }

        // get current user address
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDTO>> GetCurrentUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var Address = await _serviceManager.AuthenticationService.GetCurrentaUserAddresAsync(email!);
            return Ok(Address);
        }

        // update current user address
        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDTO>> UpdateCurrentUserAddress(AddressDTO addressDTO)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var UpdatedAddress = await _serviceManager.AuthenticationService.UpdateCurrentUserAddressAsync(email!, addressDTO);
            return Ok(UpdatedAddress);
        }

    }
}
