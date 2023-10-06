using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using JoePizza.Data;
using JoePizza.Models;
using JoePizza.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using Size = JoePizza.Models.Size;

namespace JoePizza.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger,ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _db = db;
            _httpContextAccessor = httpContextAccessor;

            List<Pizza> pizzas = new List<Pizza>();
            List<int> quantities = new List<int>();
            List<Size> sizes = new List<Size>();
            List<Toppings> toppings = new List<Toppings>();

            var p = _db.pizzasInCart?.ToList();
            if (p != null)
            {
                foreach (var item in p)
                {
                    var pizza = _db.Pizza.FirstOrDefault(c => c.Id == item.PizzaId);
                    var s = _db.Sizes.FirstOrDefault(c => c.Id == item.SizeId);
                    var t = _db.Toppings.FirstOrDefault(c => c.Id == item.ToppingId);
                    var q = item.Quantity;
                    pizzas.Add(pizza);
                    quantities.Add(q);
                    sizes.Add(s);
                    toppings.Add(t);
                }

                _httpContextAccessor.HttpContext.Session.Set("pizzas", pizzas);
                _httpContextAccessor.HttpContext.Session.Set("quantity", quantities);
                _httpContextAccessor.HttpContext.Session.Set("sizes", sizes);
                _httpContextAccessor.HttpContext.Session.Set("toppings", toppings);
            }


        }

        public IActionResult Index()
        {
            
            return View(_db.Pizza.ToList());
        }

        [HttpPost]
        public IActionResult Index(string? searchInput)
        {
            var pizzas = _db.Pizza.Where(c => c.PizzaName.Contains(searchInput)).ToList();
            if (searchInput == null)
            {
                pizzas = _db.Pizza.ToList();
            }
            
            return View(pizzas);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult Customize(int?id)
        {
            ViewData["ToppingsId"] = new SelectList(_db.Toppings.ToList(), "Id", "ToppingsName");
            ViewData["SizeId"] = new SelectList(_db.Sizes.ToList(), "Id", "PizzaSize");
            if(id== null)
            {
                return NotFound();
            }
            var pizza = _db.Pizza.Include(c=>c.Toppings).FirstOrDefault(c=>c.Id==id);
            if(pizza == null)
            {
                return NotFound();
            }
            return View(pizza);
        }

        [HttpPost]
        [ActionName("Customize")]
        public async Task<IActionResult> AddToCart(int? id,int quantity,int? size,int? topping)
        {
            List<Pizza> pizzas = new List<Pizza>();
            List<int> quantities = new List<int>();
            List<Size> sizes = new List<Size>();
            List<Toppings> toppings = new List<Toppings>();
            if(id==null)
            {
                return NotFound();
            }
            var pizza = _db.Pizza.FirstOrDefault(c=>c.Id==id);
            var s = _db.Sizes.FirstOrDefault(c=>c.Id==size);
            var t = _db.Toppings.FirstOrDefault(c => c.Id == topping);
            if(pizza == null)
            {
                return NotFound();
            }
            if (s == null)
            {
                return NotFound();
            }
            if (t == null)
            {
                return NotFound();
            }
            pizzas = HttpContext.Session.Get<List<Pizza>>("pizzas");
            sizes = HttpContext.Session.Get<List<Size>>("sizes");
            toppings = HttpContext.Session.Get<List<Toppings>>("toppings");
            quantities = HttpContext.Session.Get<List<int>>("quantity");
            if(pizzas == null)
            {
                pizzas = new List<Pizza>();
            }
            if (quantities == null)
            {
                quantities = new List<int>();
            }
            if (sizes == null)
            {
                sizes = new List<Size>();
            }
            if (toppings == null)
            {
                toppings = new List<Toppings>();
            }
            pizzas.Add(pizza);
            quantities.Add(quantity);
            sizes.Add(s);
            toppings.Add(t);
            HttpContext.Session.Set("pizzas", pizzas);
            HttpContext.Session.Set("quantity", quantities);
            HttpContext.Session.Set("sizes", sizes);
            HttpContext.Session.Set("toppings", toppings);
            PizzasInCart pic = new PizzasInCart();
            pic.PizzaId = pizza.Id;
            pic.Quantity = quantity;
            pic.SizeId = s.Id;
            pic.ToppingId = t.Id;
            _db.pizzasInCart.Add(pic);
            await _db.SaveChangesAsync();
            return View(pizza);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int? id)
        {
            List<Pizza> pizzas = HttpContext.Session.Get<List<Pizza>>("pizzas");
            List<Size> sizes = HttpContext.Session.Get<List<Size>>("sizes");
            List < Toppings > toppings = HttpContext.Session.Get<List<Toppings>>("toppings");
            List<int> quantities = HttpContext.Session.Get<List<int>>("quantity");
            if (pizzas != null)
            {
                int indexToRemove = -1;
                for (int i = 0; i < pizzas.Count; i++)
                {
                    if (pizzas[i].Id == id)
                    {
                        indexToRemove = i;
                        break;
                    }
                }

                // Remove the pizza, size, topping, and quantity at the found index
                if (indexToRemove >= 0)
                {
                    pizzas.RemoveAt(indexToRemove);
                    sizes.RemoveAt(indexToRemove);
                    toppings.RemoveAt(indexToRemove);
                    quantities.RemoveAt(indexToRemove);
                    HttpContext.Session.Set("pizzas", pizzas);
                    HttpContext.Session.Set("sizes", sizes);
                    HttpContext.Session.Set("toppings", toppings);
                    HttpContext.Session.Set("quantity", quantities);
                }
                var p = _db.pizzasInCart.FirstOrDefault(x => x.PizzaId == id);
                _db.pizzasInCart.Remove(p);
                await _db.SaveChangesAsync();

            }
            return RedirectToAction(nameof(Index));
         
        }

        [ActionName("Remove")]
        public async Task<IActionResult> RemoveFromCart(int? id)
        {
            List<Pizza> pizzas = HttpContext.Session.Get<List<Pizza>>("pizzas");
            List<Size> sizes = HttpContext.Session.Get<List<Size>>("sizes");
            List<Toppings> toppings = HttpContext.Session.Get<List<Toppings>>("toppings");
            List<int> quantities = HttpContext.Session.Get<List<int>>("quantity");
            if (pizzas != null)
            {
                int indexToRemove = -1;
                for (int i = 0; i < pizzas.Count; i++)
                {
                    if (pizzas[i].Id == id)
                    {
                        indexToRemove = i;
                        break;
                    }
                }

                // Remove the pizza, size, topping, and quantity at the found index
                if (indexToRemove >= 0)
                {
                    pizzas.RemoveAt(indexToRemove);
                    sizes.RemoveAt(indexToRemove);
                    toppings.RemoveAt(indexToRemove);
                    quantities.RemoveAt(indexToRemove);
                    HttpContext.Session.Set("pizzas", pizzas);
                    HttpContext.Session.Set("sizes", sizes);
                    HttpContext.Session.Set("toppings", toppings);
                    HttpContext.Session.Set("quantity", quantities);
                }
                var p = _db.pizzasInCart.FirstOrDefault(x => x.PizzaId == id);
                _db.pizzasInCart.Remove(p);
                await _db.SaveChangesAsync();

            }
            return RedirectToAction(nameof(Checkout));

        }

        public IActionResult Checkout()
        {
            List<Pizza> pizzas = HttpContext.Session.Get<List<Pizza>>("pizzas");
            if(pizzas == null)
            {
                pizzas = new List<Pizza>();
            }
            return View(pizzas);
        }

        public async Task<IActionResult> Order()
        {
            Order anOrder = new Order();
            List<Pizza> pizzas = HttpContext.Session.Get<List<Pizza>>("pizzas");
            List<int> quantities = HttpContext.Session.Get<List<int>>("quantity");
            List<Size> sizes = HttpContext.Session.Get<List<Size>>("sizes");
            List<Toppings> toppings = HttpContext.Session.Get<List<Toppings>>("toppings");
            decimal? tcp = 0;
            if (pizzas!=null)
            {
                
                for(int i = 0;i<pizzas.Count;i++)
                {
                    var p = pizzas[i];
                    var q = quantities[i];
                    var s = sizes[i];
                    var t = toppings[i];
                    var tpp = (p.Price+s.Price+t.Price) * q;
                    tcp += tpp;
                    OrderDetails details = new OrderDetails();
                    details.PizzaId = pizzas[i].Id;
                    details.Quantity = quantities[i];
                    details.SizeId = s.Id;
                    details.ToppingId = t.Id;
                    details.Price = tpp;
                    anOrder.OrderDetails.Add(details);
                    var ptr = _db.pizzasInCart.FirstOrDefault(c=>c.PizzaId == pizzas[i].Id);
                    _db.pizzasInCart.Remove(ptr);
                    await _db.SaveChangesAsync();    
                }
            }
            anOrder.OrderDate = DateTime.Now;
            anOrder.Amount = tcp;
            anOrder.OrderID = "PizzaNo" + (new Random().Next(1000, 9999)).ToString();
            _db.Orders.Add(anOrder);
            await _db.SaveChangesAsync();
            HttpContext.Session.Set("pizzas",new List<Pizza>());
            HttpContext.Session.Set("quantity", new List<int>());
            HttpContext.Session.Set("sizes", new List<Size>());
            HttpContext.Session.Set("toppings", new List<Toppings>());
            
            return RedirectToAction("OrderConfirmation");
        }

        public IActionResult OrderConfirmation()
        {
            var orderc = _db.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Pizzas)
                .Include(o => o.OrderDetails)
                    .ThenInclude(o => o.Size)
                .Include(o => o.OrderDetails)
                    .ThenInclude(o => o.Toppings)
                .OrderByDescending(o => o.OrderDate)
                .FirstOrDefault();
                //.ToList();
            
            return View(orderc);
        }

        public IActionResult Orders()
        {
            var orders = _db.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Pizzas)
                .Include(o => o.OrderDetails)
                    .ThenInclude(o => o.Size)
                .Include(o => o.OrderDetails)
                    .ThenInclude(o => o.Toppings)
                .ToList();
            return View(orders);
        }
    }
}