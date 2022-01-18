using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Online_store_of_digital_electronics.Models
{
    [Table("Manufacturers")]
    public class Manufacturers
    {
        [Key]
        public int Id_manufacturer { get; set; }
        [StringLength(50)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public int Number_products { get; set; }

        public ICollection<Products> Products { get; set; }
        public Manufacturers()
        {
            Products = new List<Products>();
        }

    }
}
