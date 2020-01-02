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

        // GET: /<controller>/
        public IActionResult Index()
        {

            ViewBag.cheeses = CheeseData.GetAll();

            return View();
        }


        public IActionResult Add()
        {
            return View();
        }


        public IActionResult Delete()

        {
            ViewBag.title = "Remove Cheeses";
            ViewBag.cheeses = CheeseData.GetAll();
            return View();
        }


        [HttpPost]
        [Route("/Cheese/Delete")]
        public IActionResult RemoveCheese(int[] cheeseIds)
        {
            //Remove selected cheeses from existing choices

            //Cheeses.RemoveAll(x => x.Name == cheese);
            //Cheeses.SingleOrDefault(x => x.Name == cheese);
            foreach (int cheeseId in cheeseIds)
            {
                CheeseData.Remove(cheeseId);
            }

            return Redirect("/");  //this was redirected to "/Cheese", but because we
                                   //went into the startup.cs file and changed the controller to cheese
                                   //instead of "/Home" we can redirect to index. 
        }

        [HttpPost]
        [Route("/Cheese/Add")]
        public IActionResult NewCheese(Cheese newCheese)
        {
            //Add the new cheese to the existing cheeses
            CheeseData.Add(newCheese);

            return Redirect("/Cheese");
        }

        public IActionResult Edit(int cheeseId)
        {
            ViewBag.cheese = CheeseData.GetById(cheeseId);
            return View();
        }

        [HttpPost]
        public IActionResult Edit(int id, string name, string description)
        {
            Cheese ch = CheeseData.GetById(id);
            ch.Name = name;
            ch.Description = description;
            
            return Redirect("/Cheese");
            
        }

    }
}
