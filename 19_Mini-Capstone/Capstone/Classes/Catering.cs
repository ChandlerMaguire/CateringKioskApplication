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
                    if (AccountBalance >= item.Quantity * item.Price)
                    {
                        AccountBalance -= item.Quantity * item.Price;
                        item.Quantity -= quantityInt;
                        receipt.Add(item);
                        receipt[receipt.Count - 1].Quantity = quantityInt;

                        return "Added to Cart";
                    }
                    else if(item.Code == productChoice)
                    {
                        return "Insufficient Funds";
                    }
                }
                else if(item.Code == productChoice)
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
    }
}
