using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_store_of_digital_electronics.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_store_of_digital_electronics.ViewComponents
{
    public class SwiperViewComponent : ViewComponent
    {
        private readonly ShopContext _context;

        public SwiperViewComponent(ShopContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string nameCategory)
        {
            var ProductCategory = _context.productCategories.Include(p => p.Products.Take(20)).FirstOrDefault(c => c.Name == nameCategory);
            return View(ProductCategory);
        }
    }
}
