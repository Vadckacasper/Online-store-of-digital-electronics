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

            var parents = _context.productCategories.Where(c => c.Id_parent == parentId);
            foreach (var parent in parents)
            {
                parent.Children = await _context.productCategories.Where(p => p.Id_parent == parent.Id_сategory).ToListAsync();
                productCategories.Add(parent);
            }
            return View(productCategories);
        }
    }
}
