using System.ComponentModel.DataAnnotations;

namespace Invoici.Models
{
    public class Invoice
    {
        [Key]
        public string InvoiceId { get; set; }

        [Required(ErrorMessage = "Party Name is required.")]
        [StringLength(100, ErrorMessage = "Party Name cannot exceed 100 characters.")]
        public string PartyName { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "GST Number is required.")]
        [RegularExpression(@"^\d{2}[A-Z]{5}\d{4}[A-Z]{1}[A-Z\d]{1}[Z]{1}[A-Z\d]{1}$", ErrorMessage = "Invalid GST Number format.")]
        public string GSTNumber { get; set; }

        public List<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
    }


}
