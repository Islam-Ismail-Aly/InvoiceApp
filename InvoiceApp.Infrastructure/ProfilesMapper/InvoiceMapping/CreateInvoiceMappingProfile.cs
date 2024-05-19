using AutoMapper;
using InvoiceApp.Core.DTOs.Invoice;
using InvoiceApp.Core.Entities;

namespace InvoiceApp.Infrastructure.ProfilesMapper.InvoiceMapping
{
    public class CreateInvoiceMappingProfile : Profile
    {
        public CreateInvoiceMappingProfile()
        {
            CreateMap<Invoice, InvoiceDto>()
               .ForMember(dest => dest.InvoiceNo, opt => opt.MapFrom(src => src.InvoiceNo))
               .ForMember(dest => dest.InvoiceDate, opt => opt.MapFrom(src => src.InvoiceDate))
               .ForMember(dest => dest.StoreId, opt => opt.MapFrom(src => src.Store.Id));

            CreateMap<ItemViewDto, InvoiceDetails>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Qty, opt => opt.MapFrom(src => src.Qty))
                .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount));
        }
    }
}
