using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CheeseMVC.ViewModels
{
    public class AddCheeseViewModel
    {
        [Required]
        [Display(Name = "Cheese Name")]  
        public string Name { get; set; }

        [Required(ErrorMessage = " You must give your cheese a description")]
        [Display(Name = "Cheese Description")]
        public string Description { get; set; }

        public CheeseType Type { get; set; }

        public List<SelectListItem> CheeseTypes { get; set; }


        public AddCheeseViewModel()
        {
            CheeseTypes = new List<SelectListItem>();


            // <option value="">Hard</option>   --  HTML you want generated
            CheeseTypes.Add(new SelectListItem
            {
                Value = ((int) CheeseType.Hard).ToString(),
                Text = CheeseType.Hard.ToString()
            });

            CheeseTypes.Add(new SelectListItem
            {
                Value = ((int)CheeseType.Fake).ToString(),
                Text = CheeseType.Fake.ToString()
            });

            CheeseTypes.Add(new SelectListItem
            {
                Value = ((int)CheeseType.Soft).ToString(),
                Text = CheeseType.Soft.ToString()
            });



            //How would you loop through a set of enums??
        }
    }
}
