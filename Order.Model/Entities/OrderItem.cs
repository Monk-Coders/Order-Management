using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Model.Entities
{
    [Table("order_item")]
    public class OrderItem
    {
        [Key]
        [Column("oit_id")]
        public int Id { get; set; }

        [Required]
        [Column("oit_guid")]
        public Guid OrderItemGuid { get; set; }

        [Required]
        [Column("oit_order_guid")]
        public Guid OrderGuid { get; set; }

        [Required]
        [Column("oit_product_id")]
        public Guid ProductId { get; set; }

        [Required]
        [Column("oit_quantity")]
        public int Quantity { get; set; }

        [Required]
        [Column("oit_unit_price", TypeName = "numeric(18,2)")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Column("oit_total_price", TypeName = "numeric(18,2)")]
        public decimal TotalPrice { get; set; }

        [Column("oit_is_active")]
        public bool IsActive { get; set; } = true;

        [Column("oit_is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [MaxLength(100)]
        [Column("oit_created_by")]
        public string? CreatedBy { get; set; }

        [Column("oit_created_dtt")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [MaxLength(100)]
        [Column("oit_modified_by")]
        public string? ModifiedBy { get; set; }

        [Column("oit_modified_dtt")]
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
    }
}
