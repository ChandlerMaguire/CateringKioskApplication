using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Catering
    {
        // This class should contain all the "work" for catering

        private List<CateringItem> inventory = new List<CateringItem>();
        private List<CateringItem> receipt = new List<CateringItem>();

        public decimal AccountBalance { get; set; } = 0M;

        public decimal AddMoney(string userInput)
        {
            decimal moneyToAdd = decimal.Parse(userInput);
            FileAccess file = new FileAccess();
            file.LogItems("ADD MONEY:", moneyToAdd, AccountBalance + moneyToAdd);
            return AccountBalance += moneyToAdd;
        }

        public bool CheckValidity(string userInput, decimal addedNow)
        {
            if (userInput == "1" || userInput == "5" || userInput == "10" || userInput == "20" || userInput == "50" || userInput == "100")
            {
                decimal amountToAdd = decimal.Parse(userInput);
                if (amountToAdd + addedNow > 500 || amountToAdd + AccountBalance > 1500)
                {
                    return false;
                }
                return true;
            }
            else
                return false;
        }
        public string AddToCart(string productChoice, string quantity)
        {
            int quantityInt = int.Parse(quantity);
            foreach (CateringItem item in inventory)
            {
                if (item.Code == productChoice && item.Quantity >= quantityInt)
                {
                    if (AccountBalance >= quantityInt * item.Price)
                    {
                        AccountBalance -= quantityInt * item.Price;
                        item.Quantity -= quantityInt;
                        receipt.Add(item);
                        receipt[receipt.Count - 1].Quantity = quantityInt;
                        FileAccess file = new FileAccess();
                        file.LogItems($"{quantityInt} {item.Name} {item.Code}", quantityInt * item.Price, AccountBalance);
                        return "Added to Cart";
                    }
                    else if (item.Code == productChoice)
                    {
                        return "Insufficient Funds";
                    }
                }
                else if (item.Code == productChoice)
                {
                    return "Insufficient Inventory";
                }
            }
            return "Invalid Product Code";
        }

        public List<CateringItem> StockCatering()
        {
            FileAccess file = new FileAccess();
            //List<CateringItem> inventory = new List<CateringItem>();
            inventory = file.GetItems();
            return inventory;
        }

        public List<CateringItem> AccessInventory()
        {
            return inventory;
        }

        public List<CateringItem> AccessReceipt()
        {
            return receipt;
        }
        public Dictionary<string, int> MakeChange()
        {
            int cents = (int)(AccountBalance * 100);
            if (AccountBalance == 0)
            {
                return null;
            }

            Dictionary<string, int> change = new Dictionary<string, int>();
            change["Fifties"] = cents / 5000;
            cents -= (5000 * (cents / 5000));
            change["Twenties"] = cents / 2000;
            cents -= (2000 * (cents / 2000));
            change["Tens"] = cents / 1000;
            cents -= (1000 * (cents / 1000));
            change["Fives"] = cents / 500;
            cents -= (500 * (cents / 500));
            change["Ones"] = cents / 100;
            cents -= (100 * (cents / 100));
            change["Quarters"] = cents / 25;
            cents -= (25 * (cents / 25));
            change["Dimes"] = cents / 10;
            cents -= (10 * (cents / 10));
            change["Nickles"] = cents /5;
            cents -= (5 * (cents / 5));
            foreach(KeyValuePair<string, int> kvp in change)
            {
                if(kvp.Value == 0)
                {
                    change.Remove(kvp.Key);
                }
            }
            FileAccess file = new FileAccess();
            file.LogItems("GIVE CHANGE:", AccountBalance, 0M);
            return change;

        }
    }
}
