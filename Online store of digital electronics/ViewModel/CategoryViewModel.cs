using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_store_of_digital_electronics.Models
{
    public class CategoryViewModel
    {
        public ProductCategory ProductCategory { get; set; }
        public decimal minPrice { get; set; }
        public decimal maxPrice { get; set; }

        public CategoryViewModel(ProductCategory productCategory)
        {
            ProductCategory = productCategory;
            minPrice = decimal.MaxValue;
            foreach(var product in ProductCategory.Products)
            {
                if(product.Price < minPrice)
                {
                    minPrice = product.Price;
                }
                if (product.Price > maxPrice)
                {
                    maxPrice = product.Price;
                }
            }
        }
    }
}
