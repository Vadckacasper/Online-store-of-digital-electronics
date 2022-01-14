using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_store_of_digital_electronics.Models
{
    public class RelationshipProductCategory
    {
        public int ID { get; set; }
        public int Id_product { get; set; }
        public int Id_category { get; set; }
        public Products Products { get; set; }
        public ProductCategory ProductCategory { get; set; }

    }
}
