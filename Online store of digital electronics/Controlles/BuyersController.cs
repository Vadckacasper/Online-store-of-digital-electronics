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
    public class BuyersController : Controller
    {
        private readonly ShopContext _context;
        public BuyersController(ShopContext context)
        {
            _context = context;
        }

        // GET: Buyers
        public async Task<IActionResult> Index()
        {
            return View(await _context.buyers.ToListAsync());
        }
        // GET: Buyers/Create
        public IActionResult Register()
        {
            return View();
        }

        // POST: Buyers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id_buyer,Login,Password,Contacts,IP_address,Status,Sale,Geofence")] Buyers buyers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(buyers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(buyers);
        }

        // GET: Buyers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buyers = await _context.buyers.FindAsync(id);
            if (buyers == null)
            {
                return NotFound();
            }
            return View(buyers);
        }
    }
}
