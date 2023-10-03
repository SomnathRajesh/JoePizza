using System.Diagnostics;
using JoePizza.Data;
using JoePizza.Models;
using JoePizza.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace JoePizza.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger,ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Pizza.ToList());
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
        [ActionName("AddToCart")]
        public IActionResult AddToCart(int? id,int quantity)
        {
            List<Pizza> pizzas = new List<Pizza>();
            List<int> quantities = new List<int>();
            if(id==null)
            {
                return NotFound();
            }
            var pizza = _db.Pizza.FirstOrDefault(c=>c.Id==id);
            if(pizza == null)
            {
                return NotFound();
            }
            pizzas = HttpContext.Session.Get<List<Pizza>>("pizzas");
            quantities = HttpContext.Session.Get<List<int>>("quantity");
            if(pizzas == null)
            {
                pizzas = new List<Pizza>();
            }
            if (quantities == null)
            {
                quantities = new List<int>();
            }
            pizzas.Add(pizza);
            quantities.Add(quantity);
            HttpContext.Session.Set("pizzas", pizzas);
            HttpContext.Session.Set("quantity", quantities);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Remove(int? id)
        {
            List<Pizza> pizzas = HttpContext.Session.Get<List<Pizza>>("pizzas");
            if (pizzas != null)
            {
                var pizza = pizzas.FirstOrDefault(c => c.Id == id);
                if (pizza != null)
                {
                    pizzas.Remove(pizza);
                    HttpContext.Session.Set("pizzas", pizzas);
                }
            }
            return RedirectToAction(nameof(Index));
         
        }

        [ActionName("Remove")]
        public IActionResult RemoveFromCart(int? id)
        {
            List<Pizza> pizzas = HttpContext.Session.Get<List<Pizza>>("pizzas");
            if (pizzas != null)
            {
                var pizza = pizzas.FirstOrDefault(c => c.Id == id);
                if (pizza != null)
                {
                    pizzas.Remove(pizza);
                    HttpContext.Session.Set("pizzas", pizzas);
                }
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
            decimal? tcp = 0;
            if (pizzas!=null)
            {
                
                for(int i = 0;i<pizzas.Count;i++)
                {
                    var p = pizzas[i];
                    var q = quantities[i];
                    var tpp = p.Price * q;
                    tcp += tpp;
                    OrderDetails details = new OrderDetails();
                    details.PizzaId = pizzas[i].Id;
                    //details.Pizzas.PizzaName = pizzas[i].PizzaName;
                    details.Quantity = quantities[i];
                    anOrder.OrderDetails.Add(details);
                }
            }
            anOrder.OrderDate = DateTime.Now;
            anOrder.Amount = tcp;
            anOrder.OrderID = "PizzaNo" + (new Random().Next(1000, 9999)).ToString();
            _db.Orders.Add(anOrder);
            await _db.SaveChangesAsync();
            HttpContext.Session.Set("pizzas",new List<Pizza>());
            HttpContext.Session.Set("quantity", new List<int>());
            return RedirectToAction("OrderConfirmation");
        }

        public IActionResult OrderConfirmation()
        {
            var orders = _db.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.Pizzas).ToList();
            //var orders = _db.OrderDetails.Include(o=>o.Order).Include(o=>o.Pizzas).ToList();
            return View(orders);
        }
    }
}