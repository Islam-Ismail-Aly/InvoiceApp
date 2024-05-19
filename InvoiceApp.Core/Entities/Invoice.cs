using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceApp.Core.Entities
{
    public class Invoice
    {
        [Key]
        public int InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }

        [ForeignKey("Store")]
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
        public List<InvoiceDetails> InvoiceDetails { get; set; } = new List<InvoiceDetails>();
    }
}
