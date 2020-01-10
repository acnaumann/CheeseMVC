using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CheeseMVC.Models;

namespace CheeseMVC.ViewModels
{
    public class ViewMenuViewModel
    {
        public Menu Menu { get; set; }

        public IList<CheeseMenu> Items { get; set; }


        public ViewMenuViewModel()
        {
        }
    }
}
