using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone.Classes
{
    public static class VendingMachineFileController
    {
        public static List<VendingMachineSlot> ReadIn()
        {
            string filePath = @"C:\VendingMachine\";
            string fileName = "vendingmachine.csv";
            string fullFilePath = Path.Combine(filePath, fileName);

            List<VendingMachineSlot> slots = new List<VendingMachineSlot>();

            //Possible exceptions while reading!
            using (StreamReader sr = new StreamReader(fullFilePath))
            {
                while (!sr.EndOfStream)
                {
                    //read in each line
                    string lineIn = sr.ReadLine();                

                    // parse slot name | item name | item price
                    string[] parsedLine = lineIn.Split('|');

                    //Check for errors.  Will throw exceptions if any errors are found
                    CheckReadInErrors(parsedLine, lineIn);

                    //Create slot name, item name and decimal price from parsed line
                    string slotName = parsedLine[0];
                    string itemName = parsedLine[1];
                    decimal.TryParse(parsedLine[2], out decimal itemPrice);

                    //Create a new slot containing the read in item and place it in the list of slots
                    VendingMachineSlot vms = new VendingMachineSlot(slotName);
                    vms.PlaceItemInSlot(new VendingMachineItem(itemName, itemPrice));
                    slots.Add(vms);
                }
            }
            return slots;
        }
    
        public static void UpdateLogFile(string action, decimal balance, decimal endingBalance)
        {
            string filePath = @"C:\VendingMachine\";
            string fileName = "Log.txt";
            string fullFilePath = Path.Combine(filePath, fileName);

            using (StreamWriter sw = new StreamWriter(fullFilePath, true))
            {
                string dateTimeString = DateTime.UtcNow.ToString();
                string balanceString = String.Format("{0:C2}", balance);
                string endingBalanceString = String.Format("{0:C2}", endingBalance);

                string output = String.Format(
                    "{0,-21}{1,-25}{2,-8}{3,-8}",
                    dateTimeString,
                    action,
                    balanceString,
                    endingBalanceString);
                sw.WriteLine(output);
            }
        }

        private static void CheckReadInErrors(string[] parsedLine, string lineIn)
        {
            //Check if two '|' exist (should give 3 entries)
            if (parsedLine.Length != 3)
            {
                throw new Exception(
                    $"ReadIn() Error : Wrong delimiter or number of delimiters on line \'{lineIn}\'");
            }
            //Check that item name is not empty
            if (string.IsNullOrEmpty(parsedLine[1]))
            {
                throw new Exception(
                    $"ReadIn() Error : Item name is empty with line \'{lineIn}\'");
            }
            //Check that price is a number and not blank
            if (!(decimal.TryParse(parsedLine[2], out decimal priceFound)))
            {
                throw new Exception(
                    $"ReadIn() Error : Item price is not a decimal number with line \'{lineIn}\'");
            }
            //Check that the price is positive
            if (priceFound <= 0.00M)
            {
                throw new Exception(
                    $"ReadIn() Error : Item price is negative with line \'{lineIn}\'");
            }
        }

        public static void GenerateSalesReport(VendingMachine vm)
        {
            string filePath = @"C:\VendingMachine\";
            string dateTime = $"{DateTime.Now:MM-dd-yyyy_HH_mm_ss} Sales Report.txt";
            string fullFilePath = Path.Combine(filePath, dateTime);

            using (StreamWriter sw = new StreamWriter(fullFilePath, true))
            {
                decimal totalSales = 0.00M;
                foreach (VendingMachineSlot slot in vm.Slots)
                {
                    int qtySold = 5 - slot.QuantityOfItemInSlot;
                    totalSales += qtySold * slot.ItemInSlot.Price;
                    sw.WriteLine(slot.ItemInSlot.Name + "|" + qtySold);
                }

                sw.WriteLine($"\n** TOTAL SALES **\t {totalSales:C2}");
            }
        }
    }
}
