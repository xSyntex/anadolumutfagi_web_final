using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web_Programlama_Proje.Data;
using Web_Programlama_Proje.Models;

namespace Web_Programlama_Proje.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Menu");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(x => x.Username == username && x.Password == password);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("UserId", user.Id.ToString()), // Store UserId for Orders
                    new Claim("FullName", user.FullName ?? "")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                if (user.Role == "Admin")
                {
                    return RedirectToAction("Index", "Menu");
                }
                return RedirectToAction("PublicMenu", "Menu");
            }

            ViewBag.Error = "Hatalı Ad Soyad veya Şifre!";
            return View();
        }

        public IActionResult Register()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Menu");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User model)
        {
            // Set Username same as FullName if provided via form binding to Username field
            // But wait, the view will likely bind to model properties.
            // Let's assume view binds 'Username' input to 'Username' property.
            
            // Allow duplicate names? Usually bad practice but for 'Ad Soyad' it might happen.
            // But we need a unique login. Let's assume unique for now.
            if (_context.Users.Any(u => u.Username == model.Username))
            {
                ViewBag.Error = "Bu isimde bir kullanıcı zaten var.";
                return View(model);
            }

            model.FullName = model.Username; // Sync FullName with Username
            model.Role = "User"; // Default role
            _context.Users.Add(model);
            await _context.SaveChangesAsync();

            // Auto Login after register
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Username),
                new Claim(ClaimTypes.Role, model.Role),
                new Claim("UserId", model.Id.ToString()),
                new Claim("FullName", model.FullName ?? "")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("PublicMenu", "Menu");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear(); // Clear cart on logout
            return RedirectToAction("Index", "Home");
        }
    }
}
