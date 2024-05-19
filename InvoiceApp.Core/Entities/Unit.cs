using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.Core.Entities
{
    public class Unit
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
