using Online_store_of_digital_electronics.Data;
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
        public ICollection<Products> Products { get; set; }
        public ICollection<Manufacturers> Manufacturers { get; set; }
        public ProductCategory()
        {
            Children = new List<ProductCategory>();
            Products = new List<Products>();
            Manufacturers = new List<Manufacturers>();
        }
        public List<ProductCategory> GetCatalog(ShopContext _context)
        {
            List<ProductCategory> productCategories = new List<ProductCategory>();
            List<ProductCategory> category = new List<ProductCategory>();

            var parents = _context.productCategories.Where(c => c.Id_parent == null);
            foreach (var parent in parents)
            {
                category = _context.productCategories.Where(p => p.Id_parent == parent.Id_сategory).ToList();
                parent.Children.Clear();
                foreach (var child in category)
                {

                    parent.Children.Add(child);
                }
                productCategories.Add(parent);

            }
            return productCategories;
        }
    }
}
