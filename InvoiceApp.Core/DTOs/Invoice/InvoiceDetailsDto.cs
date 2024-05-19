namespace InvoiceApp.Core.DTOs.Invoice
{
    public class InvoiceDetailsDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Qty { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
    }
}
