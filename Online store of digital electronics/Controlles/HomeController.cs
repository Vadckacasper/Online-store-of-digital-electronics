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
            List<ProductCategory> productCategories = new List<ProductCategory>();
            List<ProductCategory> category = new List<ProductCategory>();

            var parents = _context.productCategories.Where(c => c.Id_parent == null);
            foreach (var parent in parents)
            {
                category = _context.productCategories.Where(p => p.Id_parent == parent.Id_сategory).ToList();
                parent.Children.Clear();
                foreach (var child in category)
                {
                    child.Children = _context.productCategories.Where(c => c.Id_parent == child.Id_сategory).ToList();
                    parent.Children.Add(child);
                }
                productCategories.Add(parent);

            }
            HomeViewModel model = new HomeViewModel(_context.products.ToList(), productCategories.ToList());
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
