using AutoMapper;
using InvoiceApp.Core.DTOs.Invoice;
using InvoiceApp.Core.Entities;

namespace InvoiceApp.Infrastructure.ProfilesMapper.InvoiceMapping
{
    public class InvoiceMappingProfile : Profile
    {
        public InvoiceMappingProfile()
        {
            CreateMap<Invoice, InvoiceDto>()
                .ForMember(dest => dest.InvoiceNo, opt => opt.MapFrom(src => src.InvoiceNo)) 
                .ForMember(dest => dest.InvoiceDate, opt => opt.MapFrom(src => src.InvoiceDate))
                .ForMember(dest => dest.StoreId, opt => opt.MapFrom(src => src.Store.Id));
        }
    }
}
