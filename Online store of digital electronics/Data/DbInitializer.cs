using Online_store_of_digital_electronics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_store_of_digital_electronics.Data
{
    public class DbInitializer
    {
        public static void Initialize(ShopContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            //if (context.products.Any())
            //{
            //    return;   // DB has been seeded
            //}

            //var product = new Products[]
            //{
            //new Products{ Name = "mi a3", Description = "Стильный/производительный"},
            //new Products{ Name = "xiaomi mi a3", Description = "Стильный/производительный"},
            //};
            //foreach (Products s in product)
            //{
            //    context.products.Add(s);
            //}
            context.SaveChanges();


        }
    }
}
