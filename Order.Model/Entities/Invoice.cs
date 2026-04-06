using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Model.Entities
{
    [Table("invoice")]
    public class Invoice
    {
        [Key]
        [Column("inv_id")]
        public int Id { get; set; }

        [Required]
        [Column("inv_guid")]
        public Guid InvoiceGuid { get; set; }

        [Required]
        [Column("inv_order_guid")]
        public Guid OrderGuid { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("inv_invoice_number")]
        public string InvoiceNumber { get; set; } = string.Empty;

        [Column("inv_invoice_date")]
        public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;

        [MaxLength(100)]
        [Column("inv_payment_method")]
        public string? PaymentMethod { get; set; }

        [Column("inv_billing_address")]
        public string? BillingAddress { get; set; }

        [Column("inv_shipping_address")]
        public string? ShippingAddress { get; set; }

        [Required]
        [Column("inv_discount_amount", TypeName = "numeric(18,2)")]
        public decimal DiscountAmount { get; set; } = 0m;

        [Required]
        [Column("inv_total_amount", TypeName = "numeric(18,2)")]
        public decimal TotalAmount { get; set; }

        [Column("inv_due_date")]
        public DateTime? DueDate { get; set; }

        [Column("inv_is_active")]
        public bool IsActive { get; set; } = true;

        [Column("inv_is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [MaxLength(100)]
        [Column("inv_created_by")]
        public string? CreatedBy { get; set; }

        [Column("inv_created_dtt")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [MaxLength(100)]
        [Column("inv_modified_by")]
        public string? ModifiedBy { get; set; }

        [Column("inv_modified_dtt")]
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
    }
}