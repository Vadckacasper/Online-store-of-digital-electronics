using Microsoft.AspNetCore.Mvc;
using Online_store_of_digital_electronics.Data;
using Online_store_of_digital_electronics.Models;
using Online_store_of_digital_electronics.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        [HttpPost]
        public IActionResult Search(string Search)
        {
            var Category = _context.productCategories.Where(c => c.Name.StartsWith(Search) == true);
            var Product = _context.products.Where(p => p.Name.StartsWith(Search) == true);
            HomeViewModel homeViewModel = new HomeViewModel(Product.ToList(), Category.ToList());
           return PartialView("~/Views/Shared/_SearchList.cshtml", homeViewModel);
        }
    }
}
