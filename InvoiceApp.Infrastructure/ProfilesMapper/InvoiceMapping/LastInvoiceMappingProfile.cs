using AutoMapper;
using InvoiceApp.Core.DTOs.Invoice;
using InvoiceApp.Core.Entities;

namespace InvoiceApp.Infrastructure.ProfilesMapper.InvoiceMapping
{
    public class LastInvoiceMappingProfile : Profile
    {
        public LastInvoiceMappingProfile()
        {
            CreateMap<Invoice, LastInvoiceDto>()
                .ForMember(dest => dest.InvoiceNo, opt => opt.MapFrom(src => src.InvoiceNo))
                .ForMember(dest => dest.InvoiceDate, opt => opt.MapFrom(src => src.InvoiceDate))
                .ForMember(dest => dest.Store, opt => opt.MapFrom(src => src.Store != null ? new List<StoreDto> { new StoreDto { Id = src.Store.Id, Store = src.Store.Name } } : null));
        }
    }
}
