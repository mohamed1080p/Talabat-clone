using AutoMapper;
using Domain.Models.OrderModule;
using Shared.DataTransferedObjects.IdentityDTOs;
using Shared.DataTransferedObjects.OrderDTOs;
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
            CreateMap<AddressDTO, OrderAddress>().ReverseMap();
            CreateMap<Order, OrderToReturnDTO>()
                .ForMember(a => a.DeliveryMethod, a => a.MapFrom(o => o.DeliveryMethod.ShortName));


            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(a => a.ProductName, a => a.MapFrom(o => o.Product.ProductName))
                .ForMember(a => a.PictureUrl, a => a.MapFrom<OrderItemPictureUrlResolver>());

            CreateMap<DeliveryMethod, DeliveryMethodDTO>();
        }
    }
}
