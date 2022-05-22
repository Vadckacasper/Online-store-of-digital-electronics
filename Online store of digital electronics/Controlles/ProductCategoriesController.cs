using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            string BuyersID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Orders Cart = _context.orders.Include(po => po.ProductOrder).FirstOrDefault(o => o.Buyers.Id == BuyersID && o.Status == "Оформление");
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

            foreach (var order in Cart.ProductOrder)
            {
                for (int i = 0; i < Products.Count; i++)
                {
                    if (Products[i].Id_product == order.Id_product)
                    {
                        Products[i].ProductOrder.Clear();
                        Products[i].ProductOrder.Add(order);
                    }
                }
            }


            ListProduct = Products.ToList();
            return PartialView("~/Views/Products/_ProductCard_Right.cshtml", SortProduct());
        }
        // GET: ProductCategoriesTable
        public async Task<IActionResult> Index(int? id)
        {
            string BuyersID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Orders Cart = _context.orders.Include(po => po.ProductOrder).FirstOrDefault(o => o.Buyers.Id == BuyersID && o.Status == "Оформление");

            List<ProductCategory> category = _context.productCategories.Where(c => c.Id_parent == id).ToList();
            if (category.Count == 0)
            {
                if (id == null)
                {
                    return NotFound();
                }
                ProductCategory productCategory = await _context.productCategories.FirstOrDefaultAsync(m => m.Id_сategory == id);

                if (productCategory == null)
                {
                    return NotFound();
                }
                productCategory = _context.productCategories.Include(p => p.Products).ThenInclude(p => p.manufacturer).Include(p => p.Manufacturers).FirstOrDefault(c => c.Id_сategory == id);
                foreach(var order in Cart.ProductOrder)
                {
                    for(int i = 0; i < productCategory.Products.Count; i++)
                    {
                        if(productCategory.Products[i].Id_product == order.Id_product)
                        {
                            productCategory.Products[i].ProductOrder.Clear();
                            productCategory.Products[i].ProductOrder.Add(order);

                        }
                    }

                }

                CategoryViewModel categoryViewModel = new CategoryViewModel(productCategory);
                ListProduct = productCategory.Products;
                return View(categoryViewModel);
            }else
                return RedirectToAction("Category", "ProductCategories", new { id = id });
        }
        
        [HttpGet]
        public IActionResult Category(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            List<ProductCategory> productCategory =  _context.productCategories.Include(c => c.Children).Where(m => m.Id_сategory == id).ToList();

            if (productCategory == null)
            {
                return NotFound();
            }
            List<ProductCategory> categorys = new List<ProductCategory>();
            List<ProductCategory> category = new List<ProductCategory>();
            foreach (var parent in productCategory)
            {
                parent.Children.Clear();
                if (parent.Id_parent == null)
                {
                    category = _context.productCategories.Where(p => p.Id_parent == parent.Id_сategory).ToList();
                    foreach (var child in category)
                    {
                        child.Children = _context.productCategories.Where(c => c.Id_parent == child.Id_сategory).ToList();
                        categorys.Add(child);
                    }
                   
                }
                else
                {
                    parent.Children = _context.productCategories.Where(c => c.Id_parent == parent.Id_сategory).ToList();
                    categorys.Add(parent);
                }
            }
            return View(categorys);
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
