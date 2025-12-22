using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web_Programlama_Proje.Data;
using Web_Programlama_Proje.Infrastructure;
using Web_Programlama_Proje.Models;

namespace Web_Programlama_Proje.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(GetCart());
        }

        public IActionResult AddToCart(int id)
        {
            var product = _context.MenuItems.FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                var cart = GetCart();
                cart.AddItem(product, 1);
                SaveCart(cart);
                TempData["Success"] = "Ürün sepete eklendi!";
            }

            return RedirectToAction("PublicMenu", "Menu"); // Menüde kal
        }

        public IActionResult RemoveFromCart(int id)
        {
            var product = _context.MenuItems.FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                var cart = GetCart();
                cart.RemoveLine(product);
                SaveCart(cart);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Increase(int id)
        {
            var product = _context.MenuItems.FirstOrDefault(m => m.Id == id);
            if (product != null)
            {
                var cart = GetCart();
                cart.AddItem(product, 1);
                SaveCart(cart);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Decrease(int id)
        {
            var product = _context.MenuItems.FirstOrDefault(m => m.Id == id);
            if (product != null)
            {
                var cart = GetCart();
                var item = cart.Items.FirstOrDefault(x => x.MenuItem?.Id == id);
                
                if (item != null)
                {
                    if (item.Quantity > 1)
                    {
                        cart.AddItem(product, -1);
                    }
                    else
                    {
                        cart.RemoveLine(product);
                    }
                    SaveCart(cart);
                }
            }
            return RedirectToAction("Index");
        }

        [Authorize] // Sadece giriş yapmış kullanıcılar sipariş verebilir
        public async Task<IActionResult> Checkout()
        {
            var cart = GetCart();
            if (cart.Items.Count() == 0)
            {
                ModelState.AddModelError("", "Sepetiniz boş.");
                return RedirectToAction("Index");
            }

            // UserId'yi Claim'den al
            var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                TotalAmount = cart.ComputeTotalValue(),
                OrderItems = cart.Items.Select(i => new OrderItem
                {
                    MenuItemId = i.MenuItem?.Id ?? 0,
                    Quantity = i.Quantity,
                    Price = i.MenuItem?.Price ?? 0
                }).ToList()
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Sepeti temizle
            cart.Clear();
            SaveCart(cart);

            return RedirectToAction("Completed"); // Teşekkür sayfasına veya siparişlerime git
        }

        public IActionResult Completed()
        {
            return View();
        }

        private Cart GetCart()
        {
            return HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
    }
}
