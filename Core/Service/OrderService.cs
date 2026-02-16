
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.OrderModule;
using Domain.Models.ProductModule;
using ServicesAbstraction;
using Shared.DataTransferedObjects.IdentityDTOs;
using Shared.DataTransferedObjects.OrderDTOs;

namespace Services
{
    public class OrderService(IMapper _mapper, IBasketRepository _basketRepository, IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderToReturnDTO> CreateOrder(OrderDTO orderDTO, string Email)
        {
            // map AddressDTO to OrderAddress
            var OrderAddress = _mapper.Map<AddressDTO, OrderAddress>(orderDTO.Address);


            // get basket then create order item list
            var Basket = await _basketRepository.GetBasketAsync(orderDTO.BasketId) ?? throw new BasketNotFoundException(orderDTO.BasketId);
            List<OrderItem> OrderItems = new();
            var ProductRepo = _unitOfWork.GetRepository<Product, int>();
            foreach (var item in Basket.Items)
            {
                var Product = await ProductRepo.GetByIdAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);
                var orderItem = new OrderItem()
                {
                    Product = new ProductItemOrdered() { ProductId = Product.Id, PictureUrl = Product.PictureUrl, ProductName = Product.Name },
                    Price = Product.Price,
                    Quantity = item.Quantity
                };
                OrderItems.Add(orderItem);
            }


            // get delivery method
            var DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDTO.DeliveryMethodId)
                                ?? throw new DeliveryMethodNotFoundException(orderDTO.DeliveryMethodId);


            // calculate subtotal
            var SubTotal = OrderItems.Sum(a => a.Quantity * a.Price);

            var order = new Order(Email, OrderAddress, DeliveryMethod, OrderItems, SubTotal);
            await _unitOfWork.GetRepository<Order, Guid>().AddAsync(order);
            await _unitOfWork.SaveChanges();

            return _mapper.Map<Order, OrderToReturnDTO>(order);
        }
    }
}
