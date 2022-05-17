using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Online_store_of_digital_electronics.Data;
using Online_store_of_digital_electronics.Models;
using Online_store_of_digital_electronics.ViewModel;

namespace Online_store_of_digital_electronics.Controlles
{
    public class OrdersController : Controller
    {
        private readonly ShopContext _context;
        private readonly UserManager<Buyers> _userManager;
        private readonly SignInManager<Buyers> _signInManager;

        public OrdersController(ShopContext context, UserManager<Buyers> userManager, SignInManager<Buyers> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        // GET: Orders
        [HttpGet]
        public IActionResult ShoppingСart()
        {
            string BuyersID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Orders Cart = _context.orders.Include(o => o.ProductOrder).ThenInclude(p => p.Product).ThenInclude(p => p.manufacturer).FirstOrDefault(o => o.Buyers.Id == BuyersID && o.Status == "Оформление");
            return View(Cart.ProductOrder);
        }
        [HttpPost]
        public ActionResult ChangingQuantity(int id, int Number)
        {
            string BuyersID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Orders Cart = _context.orders.Include(p => p.ProductOrder).FirstOrDefault(o => o.Buyers.Id == BuyersID && o.Status == "Оформление");

            if (Number == 0)
            {
                if (_context.productOrders.FirstOrDefault(po => po.Id_product == id && po.Id_order == Cart.Id_order) != null)
                    _context.productOrders.Remove(_context.productOrders.First(po => po.Id_product == id && po.Id_order == Cart.Id_order));
            }
            else
            {
                _context.productOrders.First(po => po.Id_product == id && po.Id_order == Cart.Id_order).NumberProductsTheOrder = Number;
            }
            _context.SaveChanges();

            var productOrders = _context.productOrders.Include(p => p.Product).Where(po => po.Id_order == Cart.Id_order);
            MakingOrderViewModel making = new MakingOrderViewModel();
            foreach (var prod in productOrders)
            {
                making.SumPriceProduct += ((prod.Product.Price / 100) * (100 - prod.Product.Sale)) * prod.NumberProductsTheOrder;
                making.NumberProducts += prod.NumberProductsTheOrder;
            }
            return PartialView("~/Views/Orders/_MakingOrder.cshtml", making);
        }
        [HttpPost]
        public ActionResult AddToBasket(int id)
        {
            string BuyersID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Orders Cart = _context.orders.FirstOrDefault(o => o.Buyers.Id == BuyersID && o.Status == "Оформление");
            if (Cart == null)
            {
                Buyers buyers = _context.buyers.FirstOrDefault(b => b.Id == BuyersID);
                Cart = new Orders() { Status = "Оформление", Buyers = buyers };
                _context.orders.Add(Cart);
            }

            ProductOrder productOrder = _context.productOrders.FirstOrDefault(po => po.Id_product == id && po.Id_order == Cart.Id_order);
            if (productOrder != null)
            {
                productOrder.NumberProductsTheOrder++;
            }
            else
            {
                productOrder = new ProductOrder() { Id_order = Cart.Id_order, Id_product = id, NumberProductsTheOrder = 1, Product = _context.products.FirstOrDefault(p => p.Id_product == id), Order = Cart };
                Cart.ProductOrder.Add(productOrder);
            }
            _context.SaveChanges();

            return PartialView("~/Views/Shared/_ProductСounter.cshtml", productOrder);
        }

        [HttpPost]
        public IActionResult DeleteToBasket(int id)
        {
            string BuyersID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Orders Cart = _context.orders.Include(p => p.ProductOrder).FirstOrDefault(o => o.Buyers.Id == BuyersID && o.Status == "Оформление");

            var productOrders = _context.productOrders.Include(p => p.Product).Where(po => po.Id_order == Cart.Id_order);
            _context.productOrders.Remove(productOrders.FirstOrDefault(po => po.Id_product == id));
            _context.SaveChanges();
            MakingOrderViewModel making = new MakingOrderViewModel();
            foreach (var prod in productOrders)
            {
                making.SumPriceProduct += ((prod.Product.Price / 100) * (100 - prod.Product.Sale)) * prod.NumberProductsTheOrder;
                making.NumberProducts += prod.NumberProductsTheOrder;
            }
            return PartialView("~/Views/Orders/_MakingOrder.cshtml", making);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.orders
                .FirstOrDefaultAsync(m => m.Id_order == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_order,Id_product,Id_buyer,Number_products,Product_order,Delivery_method,Payment_method,Status,Order_name,Comment")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orders);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrder([Bind("Id_order,Id_product,Number_products,Product_order")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orders);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_order,Id_product,Id_buyer,Number_products,Product_order,Delivery_method,Payment_method,Status,Order_name,Comment")] Orders orders)
        {
            if (id != orders.Id_order)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.Id_order))
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
            return View(orders);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.orders
                .FirstOrDefaultAsync(m => m.Id_order == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orders = await _context.orders.FindAsync(id);
            _context.orders.Remove(orders);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdersExists(int id)
        {
            return _context.orders.Any(e => e.Id_order == id);
        }
    }
}
