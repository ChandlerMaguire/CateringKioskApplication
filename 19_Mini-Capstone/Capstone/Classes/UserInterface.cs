using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class UserInterface
    {
        // This class provides all user communications, but not much else.
        // All the "work" of the application should be done elsewhere

        // ALL instances of Console.ReadLine and Console.WriteLine should 
        // be in this class.
        // NO instances of Console.ReadLine or Console.WriteLIne should be
        // in any other class.

        private Catering catering = new Catering();

        public void RunInterface()
        {
            
            
            bool done = false;
            while (!done)
            {
                DisplayMainMenu();

                string userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        DisplayInventory();
                        break;
                    case "2":

                    case "3":
                    default:
                        break;
                }
            }

        }
        private void DisplayMainMenu()
        {
            Console.WriteLine("(1) Display Catering Items");
            Console.WriteLine("(2) Order");
            Console.WriteLine("(3) Quit");
        }
        private void DisplayInventory()
        {
            FileAccess file = new FileAccess();
            List<CateringItem> inventory = new List<CateringItem>();
            inventory = file.GetItems();
            Console.WriteLine("Product Code Description Qty Price");
            foreach(CateringItem item in inventory)
            {
                Console.WriteLine($"{item.Code} {item.Name} {item.Quantity} {item.Price.ToString("C")}");
            }
        }
    }
}
