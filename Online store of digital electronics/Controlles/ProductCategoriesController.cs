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
            productCategory = _context.productCategories.Include(p => p.Products).FirstOrDefault(c => c.Id_сategory == id);
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

        // GET: ProductCategoriesTable/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategory = await _context.productCategories
                .FirstOrDefaultAsync(m => m.Id_сategory == id);
            if (productCategory == null)
            {
                return NotFound();
            }

            return View(productCategory);
        }

        // GET: ProductCategoriesTable/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductCategoriesTable/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_сategory,Name,Image,Description,Available")] ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productCategory);
        }

        // GET: ProductCategoriesTable/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategory = await _context.productCategories.FindAsync(id);
            if (productCategory == null)
            {
                return NotFound();
            }
            return View(productCategory);
        }

        // POST: ProductCategoriesTable/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_сategory,Name,Image,Description,Available")] ProductCategory productCategory)
        {
            if (id != productCategory.Id_сategory)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductCategoryExists(productCategory.Id_сategory))
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
            return View(productCategory);
        }

        // GET: ProductCategoriesTable/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategory = await _context.productCategories
                .FirstOrDefaultAsync(m => m.Id_сategory == id);
            if (productCategory == null)
            {
                return NotFound();
            }

            return View(productCategory);
        }

        // POST: ProductCategoriesTable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productCategory = await _context.productCategories.FindAsync(id);
            _context.productCategories.Remove(productCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductCategoryExists(int id)
        {
            return _context.productCategories.Any(e => e.Id_сategory == id);
        }
    }
}
