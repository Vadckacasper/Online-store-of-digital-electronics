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
        private static ICollection<Products> ListProduct;
        private static string IDSorst;

        public ProductCategoriesController(ShopContext context)
        {
            _context = context;
        }


        private static List<Products> SortProduct()
        {
            if (IDSorst == "PriceOrderBy")
            {
                ListProduct = ListProduct.OrderBy(p => p.Price).ToList();
            }
            else if (IDSorst == "PriceOrderByDes")
            {
                ListProduct = ListProduct.OrderByDescending(p => p.Price).ToList();
            }
            else
            {
                ListProduct = ListProduct.OrderBy(p => p.Name).ToList();
            }
            return ListProduct.ToList();
        }
        [HttpGet]
        public IActionResult GetSortProduct(string SortID)
        {
            IDSorst = SortID;           
            return PartialView("~/Views/Products/_ProductCard_Right.cshtml", SortProduct());
        }
        [HttpGet]
        public IActionResult GetFilterProduct(int id_category, int[] Id_manufacturers, decimal minPrice = 0, decimal maxPrice = decimal.MaxValue)
        {
            var productcategory = _context.productCategories.Include(p => p.Products).ThenInclude(p => p.manufacturer).FirstOrDefault(c => c.Id_сategory == id_category);
            List<Products> Products = new List<Products>();
            foreach(var prod in productcategory.Products)
            {            
                for(int i = 0; i < Id_manufacturers.Length; i++)
                {
                    if(prod.Id_manufacturer == Id_manufacturers[i])
                    {
                        if (prod.Price >= minPrice && prod.Price <= maxPrice)
                        {
                            Products.Add(prod);
                        }
                    }
                }
            }
            ListProduct = Products.ToList();
            return PartialView("~/Views/Products/_ProductCard_Right.cshtml", SortProduct());
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
            productCategory = _context.productCategories.Include(p => p.Products).ThenInclude(p => p.manufacturer).Include(p => p.Manufacturers).FirstOrDefault(c => c.Id_сategory == id);
            ListProduct = productCategory.Products;
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
