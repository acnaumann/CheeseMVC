using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {

        private CheeseDbContext context;

        public CheeseController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {

           IList<Cheese> cheeses = context.Cheeses.Include(c => c.Category).ToList();

           return View(cheeses);
        }

        public IActionResult Delete()

        {
            ViewBag.title = "Remove Cheeses";
            ViewBag.cheeses = context.Cheeses.ToList();
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
                Cheese theCheese = context.Cheeses.Single(c => c.ID == cheeseId);
                context.Cheeses.Remove(theCheese);
            }

            context.SaveChanges();

            return Redirect("/");  //this was redirected to "/Cheese", but because we
                                   //went into the startup.cs file and changed the controller to cheese
                                   //instead of "/Home" we can redirect to index. 
        }

        public IActionResult Add()
        {
            AddCheeseViewModel addCheeseViewModel = new AddCheeseViewModel(context.Categories.ToList());
            return View(addCheeseViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddCheeseViewModel addCheeseViewModel)
        {
            if (ModelState.IsValid)
            {
                CheeseCategory newCheeseCategory =
                    context.Categories.Single(c => c.ID == addCheeseViewModel.CategoryID);
                //Cheese newCheese = new cheese()
                //{
                //    Name = addCheeseViewModel.Name,
                //    Description = addCheeseViewModel.Description,
                //    Rating = addCheeseViewModel.Rating,
                //    Category = newCheeseCategory
                //};
                

                Cheese newCheese = addCheeseViewModel.CreateCheese();
                newCheese.Category = newCheeseCategory;
                

                //context.Cheeses.Add(newCheese);
                //context.SaveChanges();

                context.Cheeses.Add(newCheese);
                context.SaveChanges();

                return Redirect("/Cheese");
            }

            AddCheeseViewModel newCheeseViewModel = new AddCheeseViewModel(context.Categories.ToList());
            return View(newCheeseViewModel);
           
        }

        //GET /Cheese/Edit? cheeseId =#
        public IActionResult Edit(int cheeseId)
        {
            Cheese ch = context.Cheeses.Single(c => c.ID == cheeseId);


            AddEditCheeseViewModel vm = new AddEditCheeseViewModel(ch, context.Categories.ToList());

            return View(vm);
        }


        //POST /Cheese/Edit
        [HttpPost]
        public IActionResult Edit(AddEditCheeseViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Cheese ch = context.Cheeses.Single(c => c.ID == vm.CheeseId);
                ch.Name = vm.Name;
                ch.Description = vm.Description;
                ch.Category = context.Categories.Single(c=> c.ID ==vm.CategoryID);
                ch.Rating = vm.Rating;

                context.SaveChanges();

            }


            return Redirect("/Cheese");

        }

    }
}
