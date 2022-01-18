using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using autosalon_.Data;
using Microsoft.EntityFrameworkCore;


namespace autosalon_.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext context;
        public HomeController(ApplicationContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await context.salon.Include(c=>c.cars).ToListAsync());
        }
        [HttpGet]
        public IActionResult AddCars()
        {
            if (context.salon.Include(c => c.cars).Where(a => a.limtCars > a.cars.Count()).Count() == 0)
            {
                ViewData["status"] = "нет доступных салонов для добавления авто";
            }
            else
                ViewData["status"] = "";
            return View();
        }
        [HttpPost]
        public IActionResult AddCars(Car car)
        {
            if (ModelState.IsValid)
            {
                AddCarsIntoDatabase(car);
                return Redirect(@"Index");
            }
            ViewData["status"] = "Не все поля заполнены";
            return View();
        }
        internal void AddCarsIntoDatabase(Car car)
        {
            double probability = 0;
            Random rand = new Random();
            List<Tuple<Salon, int>> salons =new List<Tuple<Salon, int>>();
            foreach(var sal in context.salon.Include(c => c.cars).ToList())
                {
                salons.Add(
                    new Tuple<Salon, int>(sal,
                sal.cars.Where(a => a.color == car.color && a.markname == car.markname).Count()));
                    };
            int statusCars = Math.Abs(salons.Max(a=>a.Item2) - salons.Min(a => a.Item2));
            switch (statusCars)
            {
                case 0:
                    probability = 0.5;
                    break;
                case 1:
                    probability = 0.666;
                    break;
                case 2:
                    probability = 0.8;
                    break;
                default:
                    probability = 1;
                    break;
                
            }
            if (probability == 1) 
            {
                salons.Where(a => a.Item2 == salons.Min(a => a.Item2)).FirstOrDefault().Item1.cars.Add(car);
                context.SaveChanges();
                return;
            }
            int randomWalue = rand.Next(1, 1000);
            if(randomWalue/1000 <= probability)
                salons.Where(a => a.Item2 == salons.Min(a => a.Item2)).FirstOrDefault().Item1.cars.Add(car);
            else
                salons.Where(a => a.Item2 == salons.Max(a => a.Item2)).FirstOrDefault().Item1.cars.Add(car);
            context.SaveChanges();
        }
        
    }
}
