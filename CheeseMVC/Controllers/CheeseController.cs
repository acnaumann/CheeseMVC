using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {

        private static List<Cheese> Cheeses = new List<Cheese>();

        // GET: /<controller>/
        public IActionResult Index()
        {

            ViewBag.cheeses = Cheeses;

            return View();
        }


        public IActionResult Add()
        {
            return View();
        }

        
        public IActionResult Delete()

        {
            ViewBag.cheeses = Cheeses;
            return View();
        }


        [HttpPost]
        [Route("/Cheese/Delete")]
        public IActionResult RemoveCheese(string cheese)
        {
            //Remove selected cheeses from existing choices

            //Cheeses.RemoveAll(x => x.Name == cheese);
            //Cheeses.SingleOrDefault(x => x.Name == cheese);

            foreach (Cheese cheeseObject in Cheeses)
            {
                if (cheeseObject.Name.Equals(cheese))
                {
                    Cheeses.Remove(cheeseObject);
                    break;
                }
            }

            return Redirect("/Cheese");
        }

        [HttpPost]
        [Route("/Cheese/Add")]
        public IActionResult NewCheese(string name, string description)
        {
            //create new cheese object
            Cheese ch = new Cheese(name, description);
            //Add the new cheese to the existing cheeses
            Cheeses.Add(ch);

            return Redirect("/Cheese");
        }
    }
}
