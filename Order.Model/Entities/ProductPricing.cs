using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Model.Entities
{
    [Table("product_pricing")]
    public class ProductPricing
    {
        [Key]
        [Column("pp_id")]
        public int Id { get; set; }

        [Required]
        [Column("pp_guid")]
        public Guid ProductPricingGuid { get; set; }

        [Required]
        [Column("pp_product_guid")]
        public Guid ProductGuid { get; set; }

        [Required]
        [Column("pp_price", TypeName = "numeric(18,2)")]
        public decimal Price { get; set; }

        [Required]
        [Column("pp_effective_date")]
        public DateTime EffectiveDate { get; set; } = DateTime.UtcNow;

        [Column("pp_expiry_date")]
        public DateTime? ExpiryDate { get; set; }

        [Column("pp_is_active")]
        public bool IsActive { get; set; } = true;

        [Column("pp_is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [MaxLength(100)]
        [Column("pp_created_by")]
        public string? CreatedBy { get; set; }

        [Column("pp_created_dtt")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [MaxLength(100)]
        [Column("pp_modified_by")]
        public string? ModifiedBy { get; set; }

        [Column("pp_modified_dtt")]
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
    }
}   