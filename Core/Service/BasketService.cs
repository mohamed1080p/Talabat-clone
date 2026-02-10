
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.BasketModule;
using ServiceAbstraction;
using Shared.DataTransferedObjects.BasketModuleDTOs;

namespace Service
{
    public class BasketService(IBasketRepository _basketRepository, IMapper _mapper) : IBasketService
    {
        public Task<BasketDTO> CreateOrUpdateBasketAsync(BasketDTO basket)
        {
            var CustomerBasket = _mapper.Map<BasketDTO, CustomerBasket>(basket);
            var IsCreatedOrUpdatedBasket=_basketRepository.CreateOrUpdateBasketAsync(CustomerBasket);
            if(IsCreatedOrUpdatedBasket is not null)
            {
                return GetBasketAsync(basket.Id);
            }
            else
            {
                throw new Exception("Cannot update or create basket now, try again later.");
            }
        }

        public async Task<bool> DeleteBasketAsync(string key)
        {
            return await _basketRepository.DeleteBasketAsync(key);
        }

        public async Task<BasketDTO> GetBasketAsync(string key)
        {
            var basket = await _basketRepository.GetBasketAsync(key);
            if(basket is not null)
            {
                return _mapper.Map<CustomerBasket, BasketDTO>(basket);
            }
            else
            {
                throw new BasketNotFoundException(key);
            }
        }
    }
}
