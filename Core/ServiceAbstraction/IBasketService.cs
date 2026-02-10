using Shared.DataTransferedObjects.BasketModuleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IBasketService
    {
        Task<BasketDTO> GetBasketAsync(string key);
        Task<BasketDTO> CreateOrUpdateBasketAsync(BasketDTO basket);
        Task<bool> DeleteBasketAsync(string key);
    }
}
