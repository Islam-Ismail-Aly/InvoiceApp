using AutoMapper;
using InvoiceApp.Core.DTOs.Store;
using InvoiceApp.Core.Entities;

namespace InvoiceApp.Infrastructure.ProfilesMapper.StoreMapping
{
    public class StoreMappingProfile : Profile
    {
        public StoreMappingProfile()
        {
            CreateMap<Store, StoreListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
