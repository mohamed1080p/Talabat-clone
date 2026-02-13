namespace Domain.Models.IdentityModule
{
    public class Address
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = default!;
        public string UserId { get; set; } = default!;
    }
}