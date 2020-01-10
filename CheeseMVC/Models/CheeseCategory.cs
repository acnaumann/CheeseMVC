using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CheeseMVC.Models
{
    public class CheeseCategory
    {
        public int ID { get; set; }
        public string Name { get; set; }

        
        public CheeseCategory()
        {
        }

        
        IList<Cheese> Cheeses { get; set; }








        //this autopopulated at some point. Don't know why

        //public static implicit operator CheeseCategory(List<SelectListItem> v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
