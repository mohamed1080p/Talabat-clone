using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferedObjects.IdentityDTOs
{
    public class RegisterDTO
    {
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
