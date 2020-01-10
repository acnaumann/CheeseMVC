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
    public class MenuController : Controller
    {

        private readonly CheeseDbContext context;

        public MenuController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Menu> menus = context.Menus.ToList();
            //context = CheeseDbContext;
            return View(menus);
            //return View(context.Menus.ToList());
        }

        public IActionResult Add()
        {
            AddMenuViewModel menuViewModel = new AddMenuViewModel();
            return View(menuViewModel);
        }


        [HttpPost]
        public IActionResult Add(AddMenuViewModel menuViewModel)
        {
            if (ModelState.IsValid)
            {

                Menu newMenu = new Menu()
                {
                    Name = menuViewModel.Name

                };

                context.Menus.Add(newMenu);
                context.SaveChanges();

                return Redirect("/Menu/ViewMenu/" + newMenu.ID);
            }

            return View(menuViewModel);
        }


        //Menu/AddItem/int
        public IActionResult ViewMenu(int id)
        {
            Menu menu = context.Menus.Single(m => m.ID == id);

            List<CheeseMenu> items = context.CheeseMenus.Include(item => item.Cheese)
                .Where(cm => cm.MenuID == id).ToList();


            ViewMenuViewModel vm = new ViewMenuViewModel
            {
                Menu = menu,
                Items = items
            };

            return View(vm);
        }

        public IActionResult AddItem(int id)
        {
            Menu menu = context.Menus.Single(m => m.ID == id);
            //16:36
            List<Cheese> cheeses = context.Cheeses.ToList();
            
            return View(new AddMenuItemViewModel(menu, cheeses));
        }


        [HttpPost]
        public IActionResult AddItem(AddMenuItemViewModel addMenuItem)
        {
            if (ModelState.IsValid)
            {
                int cheeseID = addMenuItem.CheeseID;
                int menuID = addMenuItem.MenuID;

                IList<CheeseMenu> existingItems = context.CheeseMenus
                    .Where(cm => cm.CheeseID == cheeseID)
                    .Where(cm => cm.MenuID == menuID).ToList();


                if (existingItems.Count == 0)
                {
                    CheeseMenu cheeseMenu = new CheeseMenu()
                    {
                        Cheese = context.Cheeses.Single(c => c.ID == cheeseID),
                        Menu = context.Menus.Single(m => m.ID == menuID)
                    };

                    context.CheeseMenus.Add(cheeseMenu);
                    context.SaveChanges();
                
                }
                return Redirect("/Menu/ViewMenu/" + addMenuItem.MenuID);

            }

            return View("/Menu");
        }



    }
}
