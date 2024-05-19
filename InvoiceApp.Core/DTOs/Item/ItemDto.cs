using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Core.DTOs.Item
{
    public class ItemDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
    }
}
