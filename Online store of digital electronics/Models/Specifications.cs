using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_store_of_digital_electronics.Models
{
    public class Specifications
    {
        public int ID { get; set; }
        public int id_product { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Products Product { get; set; }
        public ProductCategory Category { get; set; }

    }
}
