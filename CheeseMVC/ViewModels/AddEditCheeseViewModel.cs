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

        public AddEditCheeseViewModel(Cheese ch)
        {
            //Use Cheese object to initialize the ViewModel properties
            CheeseId = ch.CheeseId;
            Name = ch.Name;
            Description = ch.Description;
            Type = ch.Type;
            Rating = ch.Rating;
        }
    }
}
