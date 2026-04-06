using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Model.Entities
{
    [Table("order")]
    public class Order
    {
        [Key]
        [Column("ord_id")]
        public int Id { get; set; }

        [Required]
        [Column("ord_guid")]
        public Guid OrderGuid { get; set; }

        [Required]
        [Column("ord_customer_id")]
        public Guid CustomerId { get; set; }

        [Column("ord_ordered_dtt")]
        public DateTime OrderedDate { get; set; } = DateTime.UtcNow;

        [Required]
        [Column("ord_total_amount", TypeName = "numeric(18,2)")]
        public decimal TotalAmount { get; set; } = 0m;

        [Column("ord_item_count")]
        public int ItemCount { get; set; } = 0;

        [MaxLength(100)]
        [Column("ord_region")]
        public string? Region { get; set; }

        [Column("ord_discount", TypeName = "numeric(18,2)")]
        public decimal Discount { get; set; } = 0m;

        [Column("ord_shipping_cost", TypeName = "numeric(18,2)")]
        public decimal ShippingCost { get; set; } = 0m;

        [MaxLength(50)]
        [Column("ord_payment_method")]
        public string? PaymentMethod { get; set; }

        [Column("ord_status")]
        public Guid? Status { get; set; }

        [Column("ord_is_active")]
        public bool IsActive { get; set; } = true;

        [Column("ord_is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [MaxLength(100)]
        [Column("ord_created_by")]
        public string? CreatedBy { get; set; }

        [Column("ord_created_dtt")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [MaxLength(100)]
        [Column("ord_modified_by")]
        public string? ModifiedBy { get; set; }

        [Column("ord_modified_dtt")]
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
    }
}