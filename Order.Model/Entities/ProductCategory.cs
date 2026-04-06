using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Model.Entities
{
    [Table("product_category")]
    public class ProductCategory
    {

        [Required]
        [Column("prd_id")]
        public int Id { get; set; }
        [Required]
        [Column("prd_guid")]
        public Guid ProductCategoryGuid { get; set; }
        [Required]
        [MaxLength(255)]
        [Column("prd_name")]
        public string Name { get; set; } = string.Empty;
        [Column("prd_description")]
        public string? Description { get; set; }
        [Column("prd_created_by")]
        public string? CreatedBy { get; set; }
        [Column("prd_created_dtt")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        [Column("prd_modified_by")]
        public string? ModifiedBy { get; set; }
        [Column("prd_modified_dtt")]
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
    }

}