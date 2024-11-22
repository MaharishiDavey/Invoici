using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Invoici.Models
{
    public class InvoiceItem
    {
        [Key]
        public int InvoiceItemId { get; set; }

        [Required(ErrorMessage = "Particular is required.")]
        [StringLength(200, ErrorMessage = "Particular cannot exceed 200 characters.")]
        public string Particular { get; set; }

        [Required(ErrorMessage = "HSN Code is required.")]
        [StringLength(10, ErrorMessage = "HSN Code cannot exceed 10 characters.")]
        public string HSNCode { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Rate is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Rate must be greater than zero.")]
        public decimal Rate { get; set; }

        [NotMapped]
        public decimal Amount => Quantity * Rate;
        [ValidateNever]
        public string InvoiceId { get; set; }
        [ValidateNever]
        [ForeignKey("InvoiceId")]
        public Invoice Invoices { get; set; }

    }

}
