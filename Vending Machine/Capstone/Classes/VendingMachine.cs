using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class VendingMachine
    {
        public List<VendingMachineSlot> Slots { get; set; }
        public VendingMachineSlot SelectedSlot { get; private set; }
        public decimal MoneyInTheVendingMachine { get; private set; }

        public VendingMachine()
        {
            Initialize();
        }

        private void Initialize()
        {
            SelectedSlot = new VendingMachineSlot();
            Slots = new List<VendingMachineSlot>();
        }

        public void AddMoneyToTheVendingMachine(int amount)
        {
            if (amount > 0.0M)
            {
                MoneyInTheVendingMachine += (decimal)amount;
            }
        }

        public decimal ReturnChangeToUser()
        {
            decimal returnAmount = MoneyInTheVendingMachine;
            ResetBalanceToZero();
            return returnAmount;
        }

        public bool SelectSlot(string slotName)
        {
            foreach (VendingMachineSlot slot in Slots)
            {
                if (slot.NameOfSlot == slotName)
                {
                    SelectedSlot = slot;
                    return true;
                }
            }
            return false;
        }

        public string DispenseItem()
        {
            string result = "";

            //Check the slot row for the result string;
            if (SelectedSlot.NameOfSlot != "")
            {
                switch (SelectedSlot.NameOfSlot[0])
                {
                    case 'A':
                        result = "Crunch Crunch, Yum!";
                        break;
                    case 'B':
                        result = "Munch Munch, Yum!";
                        break;
                    case 'C':
                        result = "Glug Glug, Yum!";
                        break;
                    case 'D':
                        result = "Chew Chew, Yum!";
                        break;
                    default:
                        break;
                }
            }

            //Decrement money in the vending machine by item price
            MoneyInTheVendingMachine -= SelectedSlot.ItemInSlot.Price;

            //Take an item from the slot
            SelectedSlot.TakeItemFromSlot();

            return result;
        }

        public string GetReturnChangeString(decimal returnChange)
        {
            int quarters = 0;
            int dimes = 0;
            int nickels = 0;

            while (returnChange != 0.00M)
            {
                if (returnChange >= 0.25M)
                {
                    quarters++;
                    returnChange -= 0.25M;
                }
                else if (returnChange >= 0.10M)
                {
                    dimes++;
                    returnChange -= 0.10M;
                }
                else if (returnChange >= 0.05M)
                {
                    nickels++;
                    returnChange -= 0.05M;
                }
            }
            return $"Returning {quarters} quarters, {dimes} dimes, and {nickels} nickels";
        }

        public bool MoneyAvailableForSelectedProduct()
        {
            return (MoneyInTheVendingMachine >= SelectedSlot.ItemInSlot.Price);
        }

        private void ResetBalanceToZero()
        {
            MoneyInTheVendingMachine = 0.0M;
        }
    }
}
