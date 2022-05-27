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
    public class ProductsController : Controller
    {
        private readonly ShopContext _context;

        public ProductsController(ShopContext context)
        {
            _context = context;
        }

        // GET: ProductsTable
        public async Task<IActionResult> Index(int Id_Category)
        {
            var Products = _context.products.OrderBy(p => p.Price).Include(c => c.Categories);
            return View(await Products.ToListAsync());
        }

        [HttpGet]
        public IActionResult Search_input(string Search)
        {
            Products Product = _context.products.FirstOrDefault(c => c.Name.StartsWith(Search) == true);
            if (Product != null)
            {
                return RedirectToAction("Details", "Products", new { id = Product.Id_product });
            }
            ProductCategory productsController = _context.productCategories.FirstOrDefault(c => c.Name.StartsWith(Search) == true);
            {if(productsController!=null)
                
                return RedirectToAction("Index", "ProductCategories", new { id = productsController.Id_сategory });
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: ProductsTable/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            string BuyersID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Orders Cart = _context.orders.Include(o => o.ProductOrder).FirstOrDefault(o => o.Buyers.Id == BuyersID && o.Status == "Оформление");

            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.products.Include(m => m.manufacturer).Include(s => s.Specifications).FirstOrDefaultAsync(m => m.Id_product == id);
            products.ProductOrder.Clear();
            if (Cart.ProductOrder.FirstOrDefault(po => po.Id_product == id) != null)
                {
                products.ProductOrder.Add(Cart.ProductOrder.FirstOrDefault(po => po.Id_product == id));
            }
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: ProductsTable/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductsTable/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_product,Id_manufacturer,Name,Description,Price,Available,Specifications,Rating")] Products products)
        {
            if (ModelState.IsValid)
            {
                _context.Add(products);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }



        // GET: ProductsTable/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }

        // POST: ProductsTable/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_product,Id_manufacturer,Name,Description,Price,Available,Specifications,Rating")] Products products)
        {
            if (id != products.Id_product)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(products);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(products.Id_product))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        // GET: ProductsTable/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.products
                .FirstOrDefaultAsync(m => m.Id_product == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: ProductsTable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var products = await _context.products.FindAsync(id);
            _context.products.Remove(products);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(int id)
        {
            return _context.products.Any(e => e.Id_product == id);
        }
    }
}
