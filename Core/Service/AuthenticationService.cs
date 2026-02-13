using Domain.Exceptions;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using ServiceAbstraction;
using Shared.DataTransferedObjects.IdentityDTOs;

namespace Service
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager) : IAuthenticationService
    {
        public async Task<UserDTO> LoginAsync(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if(user is null)
            {
                throw new UserNotFoundException(loginDTO.Email);
            }
            
            var IsPasswordValid = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
            if(IsPasswordValid == true)
            {
                
                return new UserDTO()
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token = CreateTokenAsync(user)
                };
            }
            else
            {
                throw new UnauthorizedException();
            }
        }

        public async Task<UserDTO> RegisterAsync(RegisterDTO registerDTO)
        {
            var user = new ApplicationUser()
            {
                Email = registerDTO.Email,
                DisplayName = registerDTO.DisplayName,
                PhoneNumber = registerDTO.PhoneNumber,
                UserName = registerDTO.Username
            };
            var Result = await _userManager.CreateAsync(user, registerDTO.Password);
            if(Result.Succeeded)
            {
                return new UserDTO()
                {
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    Token = CreateTokenAsync(user)
                };
            }
            else
            {
                var Errors = Result.Errors.Select(a => a.Description).ToList();
                throw new BadRequestException(Errors);
            }
        }

        private static string CreateTokenAsync(ApplicationUser user)
        {
            return "TOKEN - TODO";
        }
    }
}
