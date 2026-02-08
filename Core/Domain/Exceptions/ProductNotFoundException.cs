
namespace Domain.Exceptions
{
    public sealed class ProductNotFoundException(int Id):NotFoundException($"Product with id = {Id} is not found")
    {
    }
}
