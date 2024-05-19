using AutoMapper;
using InvoiceApp.Core.DTOs.Item;
using InvoiceApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Infrastructure.ProfilesMapper.ItemMapping
{
    public class ItemMappingProfile : Profile
    {
        public ItemMappingProfile()
        {
            CreateMap<Item, ItemDto>();
        }
    }
}
