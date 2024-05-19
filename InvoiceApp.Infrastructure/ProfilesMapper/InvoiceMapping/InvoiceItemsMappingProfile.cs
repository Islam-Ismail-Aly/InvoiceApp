using AutoMapper;
using InvoiceApp.Core.DTOs.Invoice;
using InvoiceApp.Core.DTOs.Item;
using InvoiceApp.Core.Entities;

namespace InvoiceApp.Infrastructure.ProfilesMapper.InvoiceMapping
{
    public class InvoiceItemsMappingProfile : Profile
    {
        public InvoiceItemsMappingProfile()
        {
            CreateMap<Invoice, InvoiceItemsDto>()
                    .ForMember(dest => dest.InvoiceNo, opt => opt.MapFrom(src => src.InvoiceNo))
                    .ForMember(dest => dest.InvoiceDate, opt => opt.MapFrom(src => src.InvoiceDate))
                    .ForMember(dest => dest.Store, opt => opt.MapFrom(src => src.Store != null ? src.Store.Name : null))
                    .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items != null ? src.Items.Select(i => new ItemDto
                    {
                        Name = i.Name,
                        Price = i.Price,
                        Unit = i.Unit != null ? i.Unit.Name : null
                    }) : new List<ItemDto>()));

        }
    }
}
