
namespace Domain.Exceptions
{
    public sealed class AddressNotFoundException(string Username):NotFoundException($"User: {Username} has no address")
    {
    }
}
