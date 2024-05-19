using AutoMapper;
using InvoiceApp.Core.DTOs.Unit;
using InvoiceApp.Core.Entities;

namespace InvoiceApp.Infrastructure.ProfilesMapper.UnitMapping
{
    public class UnitMappingProfile : Profile
    {
        public UnitMappingProfile()
        {
            CreateMap<Unit, UnitDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
