using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Online_store_of_digital_electronics.Models
{
    [Table("ProductCategory")]
    public class ProductCategory
    {
        [Key]
        public int Id_сategory { get; set; }
        [StringLength(50)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
        public string Name { get; set; }
        public string Description { get; set; }
        //public byte[] Image { get; set; } //?
        public bool Available { get; set; }
        // Родительская категория.
        public virtual ProductCategory Parent { get; set; }
        // Дочерние категории.        
        public virtual ICollection<ProductCategory> Children { get; set; }

        // Продукты в категории.
        public ICollection<RelationshipProductCategory> RelationshipProducts { get; set; }

        public ProductCategory()
        {
            Children = new List<ProductCategory>();
            RelationshipProducts = new List<RelationshipProductCategory>();
        }
    }
}
