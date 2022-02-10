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

            catering.StockCatering();
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
                        DisplayPurchaseMenu();

                        break;
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
        public List<CateringItem> DisplayInventory()
        {
            List<CateringItem> inventory = catering.AccessInventory();
            Console.WriteLine("Product Code".PadRight(15) + "Description".PadRight(24) + "Qty".PadRight(10) + "Price");
            foreach (CateringItem item in inventory)
            {
                string soldOut = "SOLD OUT";
                if (item.Quantity == 0)
                {
                    Console.WriteLine($"{(item.Code).PadLeft(3).PadRight(14)} {item.Name.PadRight(23)} {soldOut.PadRight(9)} {item.Price.ToString("C")}");
                }
                else
                {
                    Console.WriteLine($"{(item.Code).PadLeft(3).PadRight(14)} {item.Name.PadRight(23)} {item.Quantity.ToString("D").PadRight(9)} {item.Price.ToString("C")}");
                }
            }
            Console.WriteLine("");
            return inventory;
        }
        private void DisplayPurchaseMenu()
        {
            bool run = true;
            while (run)
            {
                Console.WriteLine("(1) Add Money");
                Console.WriteLine("(2) Select Products");
                Console.WriteLine("(3) Complete Transaction");
                Console.WriteLine($"Current Account Balance: {catering.AccountBalance.ToString("C")}");
                string userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        Console.WriteLine("Please Enter Bills One By One (Max Deposit Amount: $500, Max Account Balance: $1500");
                        Console.WriteLine("(D) Finish Deposit");
                        userInput = Console.ReadLine();
                        bool done = false;
                        decimal addedNow = 0;
                        while (!done)
                        {
                            if (userInput == "d" || userInput == "D")
                            {
                                done = true;
                                continue;
                            }
                            bool valid = catering.CheckValidity(userInput, addedNow);
                            if (valid)
                            {
                                catering.AddMoney(userInput);
                                addedNow += decimal.Parse(userInput);
                                Console.WriteLine($"Current Account Balance: {catering.AccountBalance.ToString("C")}");
                            }
                            else
                            {
                                Console.WriteLine("Invalid Entry");
                            }
                            userInput = Console.ReadLine();
                        }
                        break;
                    case "2":

                        List<CateringItem> inventory = new List<CateringItem>();
                        inventory = DisplayInventory();
                        Console.WriteLine("Please Enter Product Code");
                        string productChoice = Console.ReadLine().ToUpper();
                        Console.WriteLine("Please Enter Quantity");
                        string quantity = Console.ReadLine();
                        string result = catering.AddToCart(productChoice, quantity);
                        if (result == "Added to Cart")
                        {
                            Console.WriteLine(result);
                            Console.WriteLine();
                            continue;
                        }
                        Console.WriteLine(result);
                        Console.WriteLine();
                        break;

                    case "3":
                        run = false;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
