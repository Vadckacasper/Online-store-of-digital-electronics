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
    public class ManufacturersTableController : Controller
    {
        private readonly ShopContext _context;

        public ManufacturersTableController(ShopContext context)
        {
            _context = context;
        }

        // GET: ManufacturersTable
        public async Task<IActionResult> Index()
        {

            return View(await _context.manufacturers.ToListAsync());
        }

        // GET: ManufacturersTable/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manufacturers = await _context.manufacturers
                .FirstOrDefaultAsync(m => m.Id_manufacturer == id);
            if (manufacturers == null)
            {
                return NotFound();
            }

            return View(manufacturers);
        }

        // GET: ManufacturersTable/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ManufacturersTable/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_manufacturer,Name,Description")] Manufacturers manufacturers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(manufacturers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(manufacturers);
        }

        // GET: ManufacturersTable/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manufacturers = await _context.manufacturers.FindAsync(id);
            if (manufacturers == null)
            {
                return NotFound();
            }
            return View(manufacturers);
        }

        // POST: ManufacturersTable/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_manufacturer,Name,Description")] Manufacturers manufacturers)
        {
            if (id != manufacturers.Id_manufacturer)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(manufacturers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManufacturersExists(manufacturers.Id_manufacturer))
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
            return View(manufacturers);
        }

        // GET: ManufacturersTable/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manufacturers = await _context.manufacturers
                .FirstOrDefaultAsync(m => m.Id_manufacturer == id);
            if (manufacturers == null)
            {
                return NotFound();
            }

            return View(manufacturers);
        }

        // POST: ManufacturersTable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var manufacturers = await _context.manufacturers.FindAsync(id);
            _context.manufacturers.Remove(manufacturers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManufacturersExists(int id)
        {
            return _context.manufacturers.Any(e => e.Id_manufacturer == id);
        }
    }
}
