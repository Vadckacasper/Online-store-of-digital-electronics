using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Online_store_of_digital_electronics.Data;
using Online_store_of_digital_electronics.Models;

namespace Online_store_of_digital_electronics.Controlles
{
    public class ProductCategoriesController : Controller
    {
        private readonly ShopContext _context;

        public ProductCategoriesController(ShopContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetSortProduct(int id_category, string SortID)
        {
            ProductCategory productCategory;
            if (SortID == "PriceOrderBy")
            {
                productCategory = _context.productCategories.Include(p => p.Products.OrderBy(p => p.Price)).FirstOrDefault(c => c.Id_сategory == id_category);
            }
            else if(SortID == "PriceOrderByDes")
            {
                productCategory = _context.productCategories.Include(p => p.Products.OrderByDescending(p => p.Price)).FirstOrDefault(c => c.Id_сategory == id_category);
            }else 
            {
                productCategory = _context.productCategories.Include(p => p.Products.OrderByDescending(p => p.Name)).FirstOrDefault(c => c.Id_сategory == id_category);
            }
            return PartialView("~/Views/Products/_ProductCard_Right.cshtml", productCategory.Products);
        }
        [HttpGet]
        public IActionResult GetFilterProduct(int id_category, int[] Id_manufacturers, decimal minPrice, decimal maxPrice)
        {
            List<Products> Products = new List<Products>();
            var productCategory = _context.productCategories.Include(p => p.Products).FirstOrDefault(c => c.Id_сategory == id_category);

            foreach(var prod in productCategory.Products)
            {
                
                for(int i = 0; i < Id_manufacturers.Length; i++)
                {
                    if(prod.Id_manufacturer == Id_manufacturers[i] && prod.Price >= minPrice && prod.Price <= maxPrice)
                    {
                        Products.Add(prod);
                    }
                }
            }
            return PartialView("~/Views/Products/_ProductCard_Right.cshtml", Products);
        }
        // GET: ProductCategoriesTable
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productCategory = await _context.productCategories.FirstOrDefaultAsync(m => m.Id_сategory == id);

            if (productCategory == null)
            {
                return NotFound();
            }
            productCategory = _context.productCategories.Include(p => p.Products).Include(p => p.Manufacturers).FirstOrDefault(c => c.Id_сategory == id);
            return View(productCategory);
        }

        [HttpGet]
        public async Task<PartialViewResult> GetCategory(int? parentId = null)
        {
            List<ProductCategory> productCategories = new List<ProductCategory>();

            var parents = _context.productCategories.Where(c => c.Id_parent == parentId);
            foreach (var parent in parents)
            {
                parent.Children = await _context.productCategories.Where(p => p.Id_parent == parent.Id_сategory).ToListAsync();
                productCategories.Add(parent);
            }
            return PartialView(productCategories);
        }

       
    }
}
