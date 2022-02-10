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
            
            inventory.Sort(delegate(CateringItem c1, CateringItem c2) { return c1.Code.CompareTo(c2.Code); });
            return inventory;
        }
    }
}


