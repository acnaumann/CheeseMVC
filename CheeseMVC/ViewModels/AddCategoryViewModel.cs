using System;
using System.ComponentModel.DataAnnotations;
using CheeseMVC.Models;

namespace CheeseMVC.ViewModels
{
    public class AddCategoryViewModel
    {

        public int ID { get; set; }

        [Required]
        [Display(Name= "Category Name")]
        public string Name { get; set; }

        public AddCategoryViewModel()
        {

        }

        public CheeseCategory CreateCategory()
        {
            return new CheeseCategory
            {
                Name = this.Name,
            };
        }
    }
}
