using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Online_store_of_digital_electronics.Models
{
    [Table("Orders")]
    public class Orders
    {
        [Key]
        public int Id_order { get; set; }
        public int Id_product { get; set; }
        public int Id_buyer { get; set; }
        public int Number_products { get; set; }
        public int Product_order { get; set; }
        public string Delivery_method { get; set; }
        public string Payment_method { get; set; }
        public string Status { get; set; }
        public string Order_name { get; set; }
        public string Comment { get; set; }
        public Buyers Buyers { get; set; }
        //public ICollection<RelationshipProductOrder> RelationshipProduct { get; set; }

        //public Orders()
        //{
        //    RelationshipProduct = new List<RelationshipProductOrder>();
        //}
    }
}
