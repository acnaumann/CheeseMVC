using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CheeseMVC.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {

        private CheeseDbContext Context { get; }
        private IAuthorizationService AuthorizationService { get; }
        private UserManager<IdentityUser> UserManager { get; }

        public CheeseController(CheeseDbContext dbContext,
                                IAuthorizationService authorizationService,
                                UserManager<IdentityUser>userManager) : base()
        {
            Context = dbContext;
            AuthorizationService = authorizationService;
            UserManager = userManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            //you only want to see your own data
            //IList<Cheese> cheeses = Context.Cheeses.Include(c => c.Category).ToList();

            var cheeses = from c in Context.Cheeses
                          select c;

            var isAuthorized = User.IsInRole(Constants.AdministratorsRole);

            var currentUserId = UserManager.GetUserId(User);

            if (!isAuthorized)  //so not an administrator
            {
                //if not administrator, but ARE user, select down to the cheeses with the
                //logged in userIds
                cheeses = cheeses.Where(c => c.UserID == currentUserId);
            }

            return View(cheeses.Include(c => c.Category).ToList());

           //return View(cheeses);
        }

        public IActionResult Delete()

        {
            ViewBag.title = "Remove Cheeses";
            ViewBag.cheeses = Context.Cheeses.ToList();
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
                Cheese theCheese = Context.Cheeses.Single(c => c.ID == cheeseId);
                Context.Cheeses.Remove(theCheese);
            }

            Context.SaveChanges();

            return Redirect("/");  //this was redirected to "/Cheese", but because we
                                   //went into the startup.cs file and changed the controller to cheese
                                   //instead of "/Home" we can redirect to index. 
        }

        public IActionResult Add()
        {
            AddCheeseViewModel addCheeseViewModel = new AddCheeseViewModel(Context.Categories.ToList());
            return View(addCheeseViewModel);
        }

        // TODO -- you want to add your ID to the cheese you add

        [HttpPost]
        public async Task<IActionResult> Add(AddCheeseViewModel addCheeseViewModel)
        {
            if (ModelState.IsValid)
            {
                CheeseCategory newCheeseCategory =
                    Context.Categories.Single(c => c.ID == addCheeseViewModel.CategoryID);


                Cheese newCheese = new Cheese()
                {
                    Name = addCheeseViewModel.Name,
                    Description = addCheeseViewModel.Description,
                    Rating = addCheeseViewModel.Rating,
                    Category = newCheeseCategory,
                    UserID = UserManager.GetUserId(User)

                };

                var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                    User, newCheese,
                                                    CheeseOperations.Create);

                if (!isAuthorized.Succeeded)
                {
                    return Forbid();
                }
                //Cheese newCheese = addCheeseViewModel.CreateCheese();
                //newCheese.Category = newCheeseCategory;
                

                //context.Cheeses.Add(newCheese);
                //context.SaveChanges();

                Context.Cheeses.Add(newCheese);
                await Context.SaveChangesAsync();

                return Redirect("/Cheese");
            }

            //AddCheeseViewModel newCheeseViewModel = new AddCheeseViewModel(Context.Categories.ToList());
            return View(addCheeseViewModel);
           
        }

        //GET /Cheese/Edit? cheeseId =#
        public IActionResult Edit(int cheeseId)
        {
            Cheese ch = Context.Cheeses.Single(c => c.ID == cheeseId);


            AddEditCheeseViewModel vm = new AddEditCheeseViewModel(ch, Context.Categories.ToList());

            return View(vm);
        }


        //POST /Cheese/Edit
        [HttpPost]
        public IActionResult Edit(AddEditCheeseViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Cheese ch = Context.Cheeses.Single(c => c.ID == vm.CheeseId);
                ch.Name = vm.Name;
                ch.Description = vm.Description;
                ch.Category = Context.Categories.Single(c=> c.ID ==vm.CategoryID);
                ch.Rating = vm.Rating;

                Context.SaveChanges();

            }


            return Redirect("/Cheese");

        }

    }
}
