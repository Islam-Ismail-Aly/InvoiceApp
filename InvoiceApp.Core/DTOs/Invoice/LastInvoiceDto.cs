using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Core.DTOs.Invoice
{
    public class LastInvoiceDto
    {
        public int InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public List<StoreDto>? Store { get; set; }
    }

    public class StoreDto
    {
        public int Id { get; set; }
        public string Store { get; set; }
    }
}
