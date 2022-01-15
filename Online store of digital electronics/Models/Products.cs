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
        public string Description { get; set; }
        //public List<byte[]> Image { get; set; }
        public decimal Price { get; set; }
        public bool Available { get; set; }
        public string Specifications { get; set; }
        public double Rating { get; set; }
        public Manufacturers manufacturer { get; set; }
        //Категории, в которых есть данный продукт.
        public ICollection<RelationshipProductCategory> RelationshipCategories { get; set; }
        public ICollection<RelationshipProductOrder> RelationshipOrder { get; set; }

        public Products()
        {
            RelationshipCategories = new List<RelationshipProductCategory>();
            RelationshipOrder = new List<RelationshipProductOrder>();
        }
    }
}
