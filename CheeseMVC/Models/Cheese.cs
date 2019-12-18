using System;
namespace CheeseMVC.Models
{
    public class Cheese
    {

        public string Name { get; set; }
        public string Description { get; set; }

        public Cheese(string name, string description)
        {
            Name = name;
            Description = description;
        }
        
    }
}
