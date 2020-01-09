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

        [Required]
        [Display(Name = "Category")]
        public int CategoryID { get; set; }

        public List<SelectListItem> Categories { get; set; }


        [Range(1,5)]
        public int Rating { get; set; }


        public AddCheeseViewModel(IEnumerable<CheeseCategory> categories)
        {
            Categories = new List<SelectListItem>();

            foreach (CheeseCategory category in categories)
            {
                Categories.Add(new SelectListItem
                {
                    Value = category.ID.ToString(),
                    Text = category.Name
                });
            }

            //// <option value="">Hard</option>   --  HTML you want generated
            //Categories.Add(new SelectListItem
            //{
            //    Value = ((int) CheeseType.Hard).ToString(),
            //    Text = CheeseType.Hard.ToString()
            //});

            //CheeseTypes.Add(new SelectListItem
            //{
            //    Value = ((int)CheeseType.Fake).ToString(),
            //    Text = CheeseType.Fake.ToString()
            //});

            //CheeseTypes.Add(new SelectListItem
            //{
            //    Value = ((int)CheeseType.Soft).ToString(),
            //    Text = CheeseType.Soft.ToString()
            //});

            ////How would you loop through a set of enums??
        }

        public AddCheeseViewModel()
        {

        }

        public Cheese CreateCheese()
        {
            return new Cheese
            {
                Name = this.Name,
                Description = this.Description,
                CategoryID = this.CategoryID,
                Rating = this.Rating
            };
            
        }
    }

}
