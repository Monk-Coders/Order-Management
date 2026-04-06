using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Model.Entities
{
    [Table("product")]
    public class Product
    {
        [Key]
        [Column("prd_id")]
        public int Id { get; set; }

        [Required]
        [Column("prd_guid")]
        public Guid ProductGuid { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("prd_name")]
        public string Name { get; set; } = string.Empty;

        [Column("prd_description")]
        public string? Description { get; set; }

        [Column("prd_category")]
        public Guid? CategoryGuid { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("prd_code")]
        public string Code { get; set; } = string.Empty;

        [Required]
        [Column("prd_price", TypeName = "numeric(18,2)")]
        public decimal Price { get; set; }

        [Column("prd_is_active")]
        public bool IsActive { get; set; } = true;

        [Column("prd_is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [MaxLength(100)]
        [Column("prd_created_by")]
        public string? CreatedBy { get; set; }

        [Column("prd_created_dtt")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [MaxLength(100)]
        [Column("prd_modified_by")]
        public string? ModifiedBy { get; set; }

        [Column("prd_modified_dtt")]
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
    }
}