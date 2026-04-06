using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Model.Entities
{
    [Table("order_status")]
    public class OrderStatus
    {
        [Key]
        [Column("ost_id")]
        public int Id { get; set; }

        [Required]
        [Column("ost_guid")]
        public Guid OrderStatusGuid { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("ost_name")]
        public string Name { get; set; } = string.Empty;

        [Column("ost_description")]
        public string? Description { get; set; }

        [Column("ost_is_active")]
        public bool IsActive { get; set; } = true;

        [Column("ost_is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [MaxLength(100)]
        [Column("ost_created_by")]
        public string? CreatedBy { get; set; }

        [Column("ost_created_dtt")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [MaxLength(100)]
        [Column("ost_modified_by")]
        public string? ModifiedBy { get; set; }

        [Column("ost_modified_dtt")]
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
    }
}