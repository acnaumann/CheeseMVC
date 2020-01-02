using System;
using System.ComponentModel.DataAnnotations;

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


        public AddCheeseViewModel()
        {
            
        }
    }
}
