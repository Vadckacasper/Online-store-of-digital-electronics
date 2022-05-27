using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_store_of_digital_electronics.Data;
using Online_store_of_digital_electronics.Models;
using Online_store_of_digital_electronics.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Online_store_of_digital_electronics.Controlles
{
    public class AccountController : Controller
    {
        private readonly UserManager<Buyers> _userManager;
        private readonly SignInManager<Buyers> _signInManager;
        private readonly ShopContext _context;
        public AccountController(UserManager<Buyers> userManager, SignInManager<Buyers> signInManager, ShopContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Profile()
        {
            string BuyersID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Buyers buyers = _context.buyers.Include(o => o.Orders).ThenInclude(po => po.ProductOrder).ThenInclude(p => p.Product).ThenInclude(m => m.manufacturer).FirstOrDefault(b => b.Id == BuyersID);
            //ProfileViewModel model = new ProfileViewModel(buyers);
            return View(buyers);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                Buyers user = new Buyers {FullName = model.Name, PhoneNumber = model.Phone, Email = model.Email, UserName = model.Email, };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return PartialView(model);
        }
        [HttpGet]
        public IActionResult EditPassword()
        {
            string BuyersID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            EditPasswordViewModel editPasswordViewModel = new EditPasswordViewModel();
            editPasswordViewModel.id = BuyersID;
            return View(editPasswordViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditPassword(EditPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                Buyers user = await _userManager.FindByIdAsync(model.id);
                if (user != null)
                {
                    IdentityResult result =
                        await _userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Profile");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(Buyers model)
        {
            if (ModelState.IsValid)
            {
                Buyers user = await _userManager.FindByIdAsync(model.Id);

                if (user != null)
                {
                    user.UserName = model.UserName;
                    user.Email = model.Email;
                    user.FullName = model.FullName;
                    user.PhoneNumber = model.PhoneNumber;
                    user.Geofence = model.Geofence;
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Profile");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return PartialView(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return PartialView(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return PartialView(model);
        }


    }
}
