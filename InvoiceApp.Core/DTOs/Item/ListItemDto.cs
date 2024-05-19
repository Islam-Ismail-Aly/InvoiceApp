using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Core.DTOs.Item
{
    public class ListItemDto
    {
        public List<string> Names { get; set; }
        public List<string> Units { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
    }

}
