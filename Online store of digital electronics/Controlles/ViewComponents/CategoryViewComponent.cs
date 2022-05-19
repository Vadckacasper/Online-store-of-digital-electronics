using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_store_of_digital_electronics.Data;
using Online_store_of_digital_electronics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_store_of_digital_electronics.Controlles.Component
{
    public class CategoryViewComponent:ViewComponent
    {
        private readonly ShopContext _context;

        public CategoryViewComponent(ShopContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? parentId = null)
        {
            List<ProductCategory> productCategories = new List<ProductCategory>();
            List<ProductCategory> category = new List<ProductCategory>();

            var parents = _context.productCategories.Where(c => c.Id_parent == null);
            foreach (var parent in parents)
            {
                category = await _context.productCategories.Where(p => p.Id_parent == parent.Id_сategory).ToListAsync();
                parent.Children.Clear();
                foreach (var child in category)
                {
                    child.Children = _context.productCategories.Where(c => c.Id_parent == child.Id_сategory).ToList();
                    parent.Children.Add(child);
                }
                productCategories.Add(parent);

            }
            return View(productCategories);
        }
    }
}
