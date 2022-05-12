using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_store_of_digital_electronics.Data;
using Online_store_of_digital_electronics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Online_store_of_digital_electronics.ViewComponents
{
    public class SwiperViewComponent : ViewComponent
    {
        private readonly ShopContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SwiperViewComponent(ShopContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync(string nameCategory)
        {
            string BuyersID = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Orders Cart = _context.orders.Include(o => o.ProductOrder).FirstOrDefault(o => o.Buyers.Id == BuyersID && o.Status == "Оформление");
            var ProductCategory = _context.productCategories.Include(p => p.Products.Take(20)).FirstOrDefault(c => c.Name == nameCategory);
            return View(ProductCategory);
        }
    }
}
