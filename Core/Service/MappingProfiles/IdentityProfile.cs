
using AutoMapper;
using Domain.Models.IdentityModule;
using Shared.DataTransferedObjects.IdentityDTOs;

namespace Services.MappingProfiles
{
    public class IdentityProfile:Profile
    {
        public IdentityProfile()
        {
            CreateMap<Address, AddressDTO>().ReverseMap();
        }
    }
}
