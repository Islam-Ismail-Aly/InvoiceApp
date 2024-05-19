namespace InvoiceApp.Web.ViewModels
{
    public class InvoiceViewModel
    {
        public int InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public List<string> Store { get; set; }
        public List<AddItemViewModel> Items { get; set; } = new List<AddItemViewModel>();
    }

    public class AddItemViewModel
    {
        public string Names { get; set; }
        public decimal Price { get; set; }
        public List<string> Units { get; set; } = new List<string>();
        public int Qty { get; set; }
        public decimal Discount { get; set; }
    }
}
