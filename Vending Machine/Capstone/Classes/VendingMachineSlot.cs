using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class VendingMachineSlot
    {
        public string NameOfSlot { get; set; }
        public VendingMachineItem ItemInSlot { get; private set; }
        public int QuantityOfItemInSlot { get; private set; }
        public bool IsEmpty
        {
            get
            {
                if (QuantityOfItemInSlot == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public VendingMachineSlot()
        {
            NameOfSlot = "";
        }

        public VendingMachineSlot(string nameOfSlot)
        {
            NameOfSlot = nameOfSlot;
        }

        public bool PlaceItemInSlot(VendingMachineItem vmi)
        {
            if (IsEmpty)
            {
                QuantityOfItemInSlot = 5;
                ItemInSlot = vmi;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TakeItemFromSlot()
        {
            if (!IsEmpty)
            {
                QuantityOfItemInSlot--;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool Equals(object vms)
        {
            return ((VendingMachineSlot)vms).NameOfSlot == NameOfSlot;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(NameOfSlot, ItemInSlot, QuantityOfItemInSlot, IsEmpty);
        }
    }
}
