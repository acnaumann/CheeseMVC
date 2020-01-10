using System;
using System.Collections.Generic;

namespace CheeseMVC.Models
{
    public class CheeseMenu
    {
        public int MenuID { get; set; }
        public Menu Menu { get; set; }

        public int CheeseID { get; set; }
        public Cheese Cheese { get; set; }


        //IList<CheeseMenu> CheeseMenus { get; set; }
        //this does not belong here


        public CheeseMenu()
        {
        }
    }
}
