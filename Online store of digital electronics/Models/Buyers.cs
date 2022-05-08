using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Online_store_of_digital_electronics.Models
{
    [Table("Buyers")]
    public class Buyers: IdentityUser
    {
        [Key]
        public int Id_buyer { get; set; }
        public string IP_address { get; set; }
        public string Status { get; set; }
        public string Sale { get; set; }
        public string Geofence { get; set; }
        public ICollection<Orders> Orders { get; set; }
    }
}
