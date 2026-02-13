
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferedObjects.IdentityDTOs;

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
    }
}
