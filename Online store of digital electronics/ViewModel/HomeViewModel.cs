using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Online_store_of_digital_electronics.Models;

namespace Online_store_of_digital_electronics.ViewModel
{
    public class HomeViewModel :ILayoutViewModel
    {
        public IEnumerable<Products> Products;
        public IEnumerable<ProductCategory> ProductCategories;
        public LoginViewModel Login { get; set; }
        public RegisterViewModel Register { get; set; }
        public HomeViewModel(List<Products> products, List<ProductCategory> productCategories)
        {
            Products = products;
            ProductCategories = productCategories;
        }
    }
}
