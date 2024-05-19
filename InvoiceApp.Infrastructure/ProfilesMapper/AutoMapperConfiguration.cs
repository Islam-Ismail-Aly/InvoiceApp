using InvoiceApp.Infrastructure.ProfilesMapper.InvoiceMapping;
using InvoiceApp.Infrastructure.ProfilesMapper.ItemMapping;
using InvoiceApp.Infrastructure.ProfilesMapper.StoreMapping;
using InvoiceApp.Infrastructure.ProfilesMapper.UnitMapping;
using Microsoft.Extensions.DependencyInjection;

namespace InvoiceApp.Infrastructure.ProfilesMapper
{
    public static class AutoMapperConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperConfiguration),
                                   typeof(InvoiceMappingProfile),
                                   typeof(InvoiceItemsMappingProfile),
                                   typeof(LastInvoiceMappingProfile),
                                   typeof(ItemMappingProfile),
                                   typeof(ListItemMappingProfile),
                                   typeof(CreateInvoiceMappingProfile),
                                   typeof(StoreMappingProfile),
                                   typeof(UnitMappingProfile));
        }
    }
}
