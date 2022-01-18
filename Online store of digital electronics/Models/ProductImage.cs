using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Online_store_of_digital_electronics.Models
{
    [Table("ProductsImage")]
    public class ProductImage
    {
        [Key]
        public int ID { get; set; }
        [Column(TypeName = "image")]
        public byte[] Image { get; set; }
        public Products Products { get; set; }


    }
}
