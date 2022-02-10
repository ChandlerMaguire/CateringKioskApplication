using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class CateringItem
    {
        // This class should contain the definition for one catering item
        public CateringItem(string type, string code, string name, double price)
        {
            Type = type;
            Code = code;
            Name = name;
            Price = price;
        }


        public string Type { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
        public double Price { get; private set; }
        public int Quantity { get; private set; } = 25;
    }
}
