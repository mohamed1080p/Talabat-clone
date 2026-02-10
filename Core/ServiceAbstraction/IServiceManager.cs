
namespace ServiceAbstraction
{
    public interface IServiceManager
    {
        public IProductService ProductService { get; }
        public IBasketService BasketService { get; }
    }
}
