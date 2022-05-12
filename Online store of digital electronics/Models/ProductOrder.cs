using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Online_store_of_digital_electronics.Models
{
    [Table("ProductOrder")]
    public class ProductOrder
    {
        [Key]
        public int ID { get; set; }
        public int Id_product { get; set; }
        public int Id_order{ get; set; }
        public int NumberProductsTheOrder { get; set; }
        public Products Product { get; set; }
        public Orders Order { get; set; }
    }
}
