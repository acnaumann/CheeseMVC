using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace CheeseMVC.ViewModels
{
    public class AddEditCheeseViewModel : AddCheeseViewModel
    {
        public int CheeseId { get; set; }


        public AddEditCheeseViewModel()
        {
        }

        //knowing i would have to do something similar to this in the
        //constructor it makes sense to create another constructor here

        public AddEditCheeseViewModel(Cheese ch, IEnumerable<CheeseCategory> categories) : base(categories)
        {
            //Use Cheese object to initialize the ViewModel properties
            CheeseId = ch.ID;
            Name = ch.Name;
            Description = ch.Description;
            CategoryID = ch.CategoryID;
            Rating = ch.Rating;
        }
    }
}
