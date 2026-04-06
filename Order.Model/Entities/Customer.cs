using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Model.Entities
{
    [Table("customer")]
    public class Customer
    {
        [Key]
        [Column("cst_id")]
        public int Id { get; set; }

        [Required]
        [Column("cst_guid")]
        public Guid CustomerGuid { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("cst_name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        [Column("cst_email")]
        public string Email { get; set; } = string.Empty;

        [Column("cst_address")]
        public string? Address { get; set; }

        [Column("cst_is_active")]
        public bool IsActive { get; set; } = true;

        [Column("cst_is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [MaxLength(100)]
        [Column("cst_created_by")]
        public string? CreatedBy { get; set; }

        [Column("cst_created_dtt")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [MaxLength(100)]
        [Column("cst_modified_by")]
        public string? ModifiedBy { get; set; }

        [Column("cst_modified_dtt")]
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
    }
}