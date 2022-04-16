using Microsoft.AspNetCore.Mvc;
using Online_store_of_digital_electronics.Data;
using Online_store_of_digital_electronics.Models;
using Online_store_of_digital_electronics.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_store_of_digital_electronics.Controlles
{
    public class HomeController : Controller
    {
        private readonly ShopContext _context;
        
        public HomeController(ShopContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ProductCategory productCategory = new ProductCategory();
            HomeViewModel model = new HomeViewModel(_context.products.ToList(), productCategory.GetCatalog(_context.productCategories.ToList()));
            return View(model);
        }
    }
}
