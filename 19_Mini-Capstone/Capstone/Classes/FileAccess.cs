using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone.Classes
{
    public class FileAccess
    {
        // all files for this application should in this directory
        // you will likley need to create it on your computer
        private string filePath = @"C:\Catering\cateringsystem.csv";

        // This class should contain any and all details of access to files
        public List<CateringItem> inventory = new List<CateringItem>();
        public List<CateringItem> GetItems()
        {
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] split = line.Split('|');

                        CateringItem item = new CateringItem(split[0], split[1], split[2], decimal.Parse(split[3]));

                        inventory.Add(item);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("File not found: " + ex.Message);
            }

            inventory.Sort(delegate (CateringItem c1, CateringItem c2) { return c1.Code.CompareTo(c2.Code); });
            return inventory;
        }
        private string log = @"C:\Catering\Log.txt";
        public string dateString = DateTime.Now.ToString();

        public void LogItems(string message, decimal transaction, decimal accountBalance)
        {
            using (StreamWriter sw = new StreamWriter(log, true))
            {
                sw.WriteLine($"{dateString} {message} {transaction.ToString("C")} {accountBalance.ToString("C")}");
            }
        }
        private string totalSales = @"C:\Catering\TotalSales.rpt";

        public void LogTotalSales(List<CateringItem> receipt)
        {
            List<string[]> inventory = new List<string[]>();
            Catering catering = new Catering();
            using (StreamReader sr = new StreamReader(totalSales))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    line = line.Replace("$", "");
                    string[] split = line.Split('|');
                    if (split.Length == 3)
                    {
                        inventory.Add(split);
                    }
                }

            }
            bool itemFound = false;
            foreach (CateringItem item in receipt)
            {
                foreach (string[] item2 in inventory)
                {
                    if (item2[0] == item.Name)
                    {
                        itemFound = true;
                        item2[1] = (int.Parse(item2[1]) + item.Quantity).ToString();
                        item2[2] = ((decimal.Parse(item2[2]) + (item.Quantity * item.Price)).ToString());
                    }
                }
                if (!itemFound)
                {
                    string[] temp = new string[] { item.Name, item.Quantity.ToString(), (item.Quantity * item.Price).ToString() };
                    inventory.Add(temp);
                }
                itemFound = false;
            }
            decimal total = 0;
            using (StreamWriter s = new StreamWriter(totalSales, false))
            {
                s.Write("");
            }
            foreach (string[] item in inventory)
            {
                using (StreamWriter sw = new StreamWriter(totalSales, true))
                {
                    sw.WriteLine($"{item[0]}|{item[1]}|${item[2]}");
                    total += decimal.Parse(item[2]);
                }
            }
            using (StreamWriter sw = new StreamWriter(totalSales, true))
            {
                sw.WriteLine();
                sw.WriteLine($"**TOTAL SALES** {total.ToString("C")}");
            }
        }
    }
}


