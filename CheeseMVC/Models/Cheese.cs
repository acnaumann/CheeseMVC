using System;
namespace CheeseMVC.Models
{
    public class Cheese
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public CheeseType Type { get; set; }


        public int CheeseId { get; set; }
        private static int nextId = 1;

        


        //add a default constructor - has no properties, no
        public Cheese()
        {
            CheeseId = nextId;
            nextId++;
        }
        
    }
}
