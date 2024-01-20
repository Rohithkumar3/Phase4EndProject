using Microsoft.AspNetCore.Mvc;
using PhaseEndProject.Models;

namespace PhaseEndProject.Controllers
{
    public class PizzaController : Controller
    {
        static public List<Pizza> pizzadetails = new List<Pizza>() {

                new Pizza { PizzaId = 1,Type = "Brick Oven Pizza", Price =400},
                new Pizza { PizzaId = 2,Type = "Italian Pizza",Price=350},
                new Pizza { PizzaId = 3,Type = "Greek Pizza",Price=300}
            };
        static public List<Orderinfo> orderdetails = new List<Orderinfo>();
        public IActionResult Index()
        {
            return View(pizzadetails);
        }
        public IActionResult Create()
        {
            return View(new Pizza());
        }
        public IActionResult Cart(int id)
        {
            var found = (pizzadetails.Find(p => p.PizzaId == id));

            TempData["id"] = id;

            return View(found);

        }
        [HttpPost]
        public IActionResult Cart(IFormCollection f)
        {
            Random r = new Random();
            int id = Convert.ToInt32(TempData["id"]);
            Orderinfo o = new Orderinfo();
            var found = (pizzadetails.Find(p => p.PizzaId == id));
            o.OrderId = r.Next(100, 999);
            o.PizzaId = id;
            o.Price = found.Price;
            o.Type = found.Type;
            o.Quantity = Convert.ToInt32(Request.Form["qty"]);
            o.TotalPrice = o.Price * o.Quantity;
            orderdetails.Add(o);

            return RedirectToAction("Checkout");
        }


        public IActionResult Checkout()
        {
            return View(orderdetails);
        }
    }
}
