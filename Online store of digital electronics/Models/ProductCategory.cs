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
        public string Image { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
        public int? Id_parent { get; set; }
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
        public List<ProductCategory> GetCatalog(List<ProductCategory> AllCategories)
        {
            List<ProductCategory> productCategories = new List<ProductCategory>();

            var parents = AllCategories.Where(c => c.Id_parent == null);
            foreach (var parent in parents)
            {
                parent.Children = AllCategories.Where(p => p.Id_parent == parent.Id_сategory).ToList();
                productCategories.Add(parent);
            }
            return productCategories;
        }

        private List<ProductCategory> GetCheldren(ProductCategory parent)
        {

            return null;
        }
    }
}
