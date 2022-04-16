using Online_store_of_digital_electronics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_store_of_digital_electronics.ViewModel
{
    public class ManufactureViewModel
    {
        public IEnumerable<Manufacturers> Manufacturers;
        public IEnumerable<ProductCategory> ProductCategories;

        public ManufactureViewModel(List<Manufacturers> manufacturers, List<ProductCategory> productCategories)
        {
            Manufacturers = manufacturers;
            ProductCategories = productCategories;
        }
    }
}
