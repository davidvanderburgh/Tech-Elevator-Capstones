using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone.Classes
{
    public class UserInterface
    {
        private VendingMachine vendingMachine = new VendingMachine();

        public void RunInterface()
        {
            bool done = false;
            if (!InitializeVendingMachine())
            {
                done = true;
            }

            while (!done)
            {
                Console.WriteLine("(1) Display Vending Machine Items\n(2) Purchase\n(3) End");
                string inputEntry = Console.ReadLine();

                if (!(inputEntry == "1" || inputEntry == "2" || inputEntry == "3" || inputEntry == "9"))
                {
                    Console.WriteLine("Invalid entry, try again");
                    continue;
                }

                switch (inputEntry)
                {
                    case "1":
                        DisplayVendingMachineItems();
                        break;
                    case "2":
                        Purchase();
                        break;
                    case "3":
                        End();
                        done = true;
                        break;
                    case "9":
                        SalesReport();
                        break;
                    default:
                        continue;
                }
            }

            Console.ReadLine();
        }

        private bool InitializeVendingMachine()
        {
            try
            {
                vendingMachine.Slots = VendingMachineFileController.ReadIn();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("FATAL ERROR: Problems parsing the read in file!");
                Console.WriteLine(e.Message);
                return false;
            }
        }

        private void DisplayVendingMachineItems()
        {
            foreach (VendingMachineSlot vms in vendingMachine.Slots)
            {
                string qty = vms.QuantityOfItemInSlot.ToString();
                if (qty == "0")
                {
                    qty = "SOLD OUT";
                }
                Console.WriteLine($"{vms.NameOfSlot}|{vms.ItemInSlot.Name}|" +
                    $"{vms.ItemInSlot.Price:C2}|QTY={qty}");
            }
            Console.WriteLine();
        }

        private void Purchase()
        {
            bool done = false;

            while (!done)
            {
                Console.WriteLine("(1) Feed Money\n(2) Select Product\n(3) Finish Transaction");
                Console.WriteLine($"Current Money Provided: {vendingMachine.MoneyInTheVendingMachine:C2}");
                string inputEntry = Console.ReadLine();

                switch (inputEntry)
                {
                    case "1":
                        FeedMoney();
                        break;
                    case "2":
                        SelectProduct();
                        break;
                    case "3":
                        FinishTransaction();
                        done = true;
                        break;
                    default:
                        Console.WriteLine("Invalid entry, try again");
                        continue;
                }
            }
        }

        private void FeedMoney()
        {
            Console.Write("Enter a bill: ");
            string billEntered = Console.ReadLine();
            if (!(int.TryParse(billEntered, out int bill)))
            {
                Console.WriteLine("Bill entered is not a number");
            }
            else
            {
                if (!(bill == 1 || bill == 2 || bill == 5 || bill == 10))
                {
                    Console.WriteLine("Bill entered must be: 1, 2, 5 or 10");
                }
                else
                {
                    vendingMachine.AddMoneyToTheVendingMachine(bill);

                    UpdateLogFile("FEED MONEY:", bill * 1.00M, vendingMachine.MoneyInTheVendingMachine);
                }
            }
        }

        private void SelectProduct()
        {
            DisplayVendingMachineItems();
            Console.WriteLine("Enter your slot name (A2 for example):");
            string selection = Console.ReadLine();

            if (!vendingMachine.SelectSlot(selection))
            {
                Console.WriteLine("Invalid entry selected");
            }
            else if (vendingMachine.SelectedSlot.IsEmpty)
            {
                Console.WriteLine("Item is sold out!");
            }
            else
            {
                decimal balanceBefore = vendingMachine.MoneyInTheVendingMachine;
                
                if (vendingMachine.MoneyAvailableForSelectedProduct())
                {
                    Console.WriteLine(vendingMachine.DispenseItem() + "\n");
                    UpdateLogFile(vendingMachine.SelectedSlot.ItemInSlot.Name, balanceBefore, 
                        vendingMachine.MoneyInTheVendingMachine);
                }
                else
                {
                    Console.WriteLine("Not enough money to purchase item");
                }
            }
        }

        private void FinishTransaction()
        {
            decimal returnChange = vendingMachine.ReturnChangeToUser();
            Console.WriteLine(vendingMachine.GetReturnChangeString(returnChange));

            UpdateLogFile("GIVE CHANGE:", returnChange, vendingMachine.MoneyInTheVendingMachine);
        }

        private void SalesReport()
        {
            try
            {
                VendingMachineFileController.GenerateSalesReport(vendingMachine);
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR in generate sales report : " + e.Message);
            }
        }

        private void UpdateLogFile(string action, decimal balance, decimal endingBalance)
        {
            try
            {
                VendingMachineFileController.UpdateLogFile(action, balance, endingBalance);
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR updating Log file : " + e.Message);
            }
        }

        private void End()
        {
            Console.WriteLine("Ending interface!");
        }
    }
}
