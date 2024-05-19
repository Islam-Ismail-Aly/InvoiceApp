namespace InvoiceApp.Web.ViewModels
{
    public class ItemViewModel
    {
        public List<string> Names { get; set; }
        public List<string> Units { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
    }
}
