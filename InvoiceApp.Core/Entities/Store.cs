using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.Core.Entities
{
    public class Store
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Invoice> Invoices  { get; set; } = new List<Invoice>();
    }
}
