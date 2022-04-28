using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Online_store_of_digital_electronics.Models
{
    [Table("Products")]
    public class Products
    {
        [Key]
        public int Id_product { get; set; }
        public int Id_manufacturer { get; set; }

        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Available { get; set; }
        public string Specifications { get; set; }
        public double Rating { get; set; }
        public Manufacturers manufacturer { get; set; }
        //Категории, в которых есть данный продукт.
        public ICollection<ProductCategory> Categories { get; set; }
        public ICollection<Orders> Order { get; set; }
        public Products()
        {
            Categories = new List<ProductCategory>();
            Order = new List<Orders>();
        }
    }
}
