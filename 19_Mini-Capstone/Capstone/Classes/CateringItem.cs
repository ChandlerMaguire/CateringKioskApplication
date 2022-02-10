using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class CateringItem
    {
        // This class should contain the definition for one catering item
        public CateringItem(string type, string code, string name, decimal price)
        {
            if (type == "B")
            {
                Type = "Beverage";
            }
            else if (type == "E")
            {
                Type = "Entree";
            }
            else if (type == "D")
            {
                Type = "Dessert";
            }
            else if (type == "A")
            {
                Type = "Appetizer";
            }
            Code = code;
            Name = name;
            Price = price;
        }


        public string Type { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; set; } = 25;

        public string Message { get
            {
                if (Type == "Beverage")
                {
                    return "Don't Forget Ice.";
                }
                else if (Type == "Entree")
                {
                    return "Did you remember Dessert?";
                }
                else if (Type == "Dessert")
                {
                    return "Coffee goes with dessert.";
                }
                else
                {
                    return "You might need extra plates.";
                }
            }
        }
    }
}
