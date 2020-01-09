using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CheeseDbContext context;

        public CategoryController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            List<CheeseCategory> categories = context.Categories.ToList();
            return View(categories);
            //return View(context.Categories.ToList();
        }

        public IActionResult Add()
        {
            AddCategoryViewModel cvm = new AddCategoryViewModel();
            return View(cvm);
        }

        [HttpPost]
        public IActionResult Add(AddCategoryViewModel cvm)
        {
            if (ModelState.IsValid)
            {
                CheeseCategory newCategory = cvm.CreateCategory();

                context.Categories.Add(newCategory);
                context.SaveChanges();
                return Redirect("/Category");
            }
            return View(cvm);
        }

    }
}




//Does the code pasted here need to go in any other controller
