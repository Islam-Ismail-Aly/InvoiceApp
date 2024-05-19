using AutoMapper;
using InvoiceApp.Core.DTOs.Item;
using InvoiceApp.Core.Entities;

namespace InvoiceApp.Infrastructure.ProfilesMapper.ItemMapping
{
    public class ListItemMappingProfile : Profile
    {
        public ListItemMappingProfile()
        {
            CreateMap<Item, ListItemDto>()
                .ForMember(dest => dest.Names, opt => opt.MapFrom(src => src.Name != null ? new List<string> { src.Name } : new List<string>()))
                .ForMember(dest => dest.Units, opt => opt.MapFrom(src => src.Unit != null ? new List<string> { src.Unit.Name } : new List<string>()))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
        }
    }
}
