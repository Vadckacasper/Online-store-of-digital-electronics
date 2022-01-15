using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Online_store_of_digital_electronics.Models
{
    [Table("RelationshipProductCategory")]
    public class RelationshipProductCategory
    {
        [Key]
        public int ID { get; set; }
        public int Id_product { get; set; }
        public int Id_category { get; set; }
        public Products Products { get; set; }
        public ProductCategory ProductCategory { get; set; }

    }
}
