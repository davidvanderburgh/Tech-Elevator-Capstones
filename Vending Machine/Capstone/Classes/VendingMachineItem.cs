using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class VendingMachineItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public VendingMachineItem()
        {

        }

        public VendingMachineItem(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}
