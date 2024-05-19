using InvoiceApp.Core.DTOs.Item;

namespace InvoiceApp.Core.DTOs.Invoice
{
    public class InvoiceItemsDto
    {
        public int InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string? Store { get; set; }
        public List<ItemDto>? Items { get; set; }
    }
}
