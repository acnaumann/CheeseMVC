using System;
using System.Collections.Generic;

namespace CheeseMVC.Models
{
    public class Menu
    {
        public int ID { get; set; }
        public string Name { get; set; }

        IList<CheeseMenu> CheeseMenus { get; set; }


        public Menu()
        {
        }
    }
}
