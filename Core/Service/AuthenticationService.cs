using AutoMapper;
using Domain.Exceptions;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.DataTransferedObjects.IdentityDTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager, IConfiguration _configuration, IMapper _mapper) : IAuthenticationService
    {
        public async Task<bool> CheckEmailAsync(string Email)
        {
            var User = await _userManager.FindByEmailAsync(Email);
            return User is not null;
        }

        public async Task<AddressDTO> GetCurrentaUserAddresAsync(string Email)
        {
            var User = await _userManager.Users.Include(a => a.Address).FirstOrDefaultAsync(a => a.Email == Email);
            if(User is null)
            {
                throw new UserNotFoundException(Email);
            }
            if(User.Address is not null)
            {
                _mapper.Map<Address, AddressDTO>(User.Address);
            }
            throw new AddressNotFoundException(User.UserName);
            
        }

        public async Task<UserDTO> GetCurrentUserAsync(string Email)
        {
            var User = await _userManager.FindByEmailAsync(Email) ?? throw new UserNotFoundException(Email);
            return new UserDTO()
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = await CreateTokenAsync(User)
            };

        }
        public async Task<AddressDTO> UpdateCurrentUserAddressAsync(string Email, AddressDTO addressDTO)
        {
            var User = await _userManager.Users.Include(a => a.Address).FirstOrDefaultAsync(a => a.Email == Email);
            if (User is null)
            {
                throw new UserNotFoundException(Email);
            }
            if(User.Address is not null)
            {
                User.Address.FirstName = addressDTO.FirstName;
                User.Address.LastName = addressDTO.LastName;
                User.Address.City = addressDTO.City;
                User.Address.Country = addressDTO.Country;
                User.Address.Street = addressDTO.Street;
            }
            else
            {
                User.Address = _mapper.Map<AddressDTO, Address>(addressDTO);

            }
            await _userManager.UpdateAsync(User);
            return _mapper.Map<AddressDTO>(User.Address);


        }

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
                    Token = await CreateTokenAsync(user)
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
                    Token = await CreateTokenAsync(user)
                };
            }
            else
            {
                var Errors = Result.Errors.Select(a => a.Description).ToList();
                throw new BadRequestException(Errors);
            }
        }


        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var Claims = new List<Claim>()
            {
                new(ClaimTypes.Email,user.Email!),
                new(ClaimTypes.Name, user.UserName!),
                new(ClaimTypes.NameIdentifier, user.Id)
            };
            var Roles = await _userManager.GetRolesAsync(user);
            foreach (var item in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, item));
            }
            var SecretKey = _configuration.GetSection("JWTOptions")["SecretKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken
                (
                issuer: _configuration["JWTOptions:Issuer"],
                audience: _configuration["JWTOptions:Audience"],
                claims: Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: Creds
                );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
