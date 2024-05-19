using InvoiceApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Core.DTOs.Invoice
{
    public class InvoiceDto
    {
        public int InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int StoreId { get; set; }
        public List<ItemViewDto> Items { get; set; } = new List<ItemViewDto>();
    }

    public class ItemViewDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<string> Units { get; set; } = new List<string>();
        public int Qty { get; set; }
        public decimal Discount { get; set; }
    }
}
