using AutoMapper;
using Domain.Models.OrderModule;
using Shared.DataTransferedObjects.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    internal class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDTO, OrderAddress>();
        }
    }
}
