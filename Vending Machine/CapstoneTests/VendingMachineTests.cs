using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;
using System.Collections.Generic;

namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineTests
    {
        VendingMachine vm;
        List<VendingMachineSlot> slots;

        [TestInitialize]
        public void Initialize()
        {
            vm = new VendingMachine();

            slots = new List<VendingMachineSlot>
            {
                new VendingMachineSlot("A1"),
                new VendingMachineSlot("A2"),
                new VendingMachineSlot("A3"),
                new VendingMachineSlot("A4"),
                new VendingMachineSlot("B1"),
                new VendingMachineSlot("B2"),
                new VendingMachineSlot("B3"),
                new VendingMachineSlot("B4"),
                new VendingMachineSlot("C1"),
                new VendingMachineSlot("C2"),
                new VendingMachineSlot("C3"),
                new VendingMachineSlot("C4"),
                new VendingMachineSlot("D1"),
                new VendingMachineSlot("D2"),
                new VendingMachineSlot("D3"),
                new VendingMachineSlot("D4")
            };

            VendingMachineItem vmi = new VendingMachineItem("name", 1.00M);
            foreach (VendingMachineSlot vms in slots)
            {
                vms.PlaceItemInSlot(vmi);

            }
            vm.Slots = slots;
        }

        [TestMethod]
        public void AddMoneyToVendingMachine_SomeBills_UpdatedMoney()
        {
            vm = new VendingMachine();
            vm.AddMoneyToTheVendingMachine(1);
            Assert.AreEqual(1.00M, vm.MoneyInTheVendingMachine);

            vm.AddMoneyToTheVendingMachine(2);
            Assert.AreEqual(3.00M, vm.MoneyInTheVendingMachine);

            vm.AddMoneyToTheVendingMachine(5);
            Assert.AreEqual(8.00M, vm.MoneyInTheVendingMachine);

            vm.AddMoneyToTheVendingMachine(10);
            Assert.AreEqual(18.00M, vm.MoneyInTheVendingMachine);
        }

        [TestMethod]
        public void AddMoneyToVendingMachine_InvalidAmount_SameMoney()
        {
            vm = new VendingMachine();
            vm.AddMoneyToTheVendingMachine(1);
            vm.AddMoneyToTheVendingMachine(-5);
            Assert.AreEqual(1.00M, vm.MoneyInTheVendingMachine);
        }

        [TestMethod]
        public void ReturnChangeToUser_MoneyExists_AllOfMoney()
        {
            vm = new VendingMachine();
            vm.AddMoneyToTheVendingMachine(1);
            Assert.AreEqual(1.00M, vm.ReturnChangeToUser());
            Assert.AreEqual(0.00M, vm.MoneyInTheVendingMachine);

            vm.AddMoneyToTheVendingMachine(5);
            Assert.AreEqual(5.00M, vm.ReturnChangeToUser());
            Assert.AreEqual(0.00M, vm.MoneyInTheVendingMachine);
        }

        [TestMethod]
        public void ReturnChangeToUser_NoMoneyExists_NoMoney()
        {
            vm = new VendingMachine();
            Assert.AreEqual(0.00M, vm.ReturnChangeToUser());
            Assert.AreEqual(0.00M, vm.MoneyInTheVendingMachine);
        }

        [TestMethod]
        public void SelectSlot_ValidChoice_True()
        {
            Assert.AreEqual(true, vm.SelectSlot("A2"));
            Assert.AreEqual(true, vm.SelectSlot("B2"));
            Assert.AreEqual(true, vm.SelectSlot("D4"));
            Assert.AreEqual(true, vm.SelectSlot("A1"));
            Assert.AreEqual(true, vm.SelectSlot("C3"));
        }

        [TestMethod]
        public void SelectSlot_InvalidChoice_False()
        {
            vm = new VendingMachine();
            Assert.AreEqual(false, vm.SelectSlot("F2"));
            Assert.AreEqual(false, vm.SelectSlot("B6"));
            Assert.AreEqual(false, vm.SelectSlot("d2"));
            Assert.AreEqual(false, vm.SelectSlot("1A"));
            Assert.AreEqual(false, vm.SelectSlot("qq"));
        }

        [TestMethod]
        public void DispenseItem_ARow_CrunchCrunchYum()
        {        
            //Must have enough money to allow dispense function
            vm.AddMoneyToTheVendingMachine(10);
            vm.AddMoneyToTheVendingMachine(10);
            vm.AddMoneyToTheVendingMachine(10);

            vm.SelectSlot("A1");
            Assert.AreEqual("Crunch Crunch, Yum!", vm.DispenseItem());
            vm.SelectSlot("A2");
            Assert.AreEqual("Crunch Crunch, Yum!", vm.DispenseItem());
            vm.SelectSlot("A3");
            Assert.AreEqual("Crunch Crunch, Yum!", vm.DispenseItem());
            vm.SelectSlot("A4");
            Assert.AreEqual("Crunch Crunch, Yum!", vm.DispenseItem());
        }
        [TestMethod]
        public void DispenseItem_BRow_MunchMunchYum()
        {
            //Must have enough money to allow dispense function
            vm.AddMoneyToTheVendingMachine(10);
            vm.AddMoneyToTheVendingMachine(10);
            vm.AddMoneyToTheVendingMachine(10);

            vm.SelectSlot("B1");
            Assert.AreEqual("Munch Munch, Yum!", vm.DispenseItem());
            vm.SelectSlot("B2");
            Assert.AreEqual("Munch Munch, Yum!", vm.DispenseItem());
            vm.SelectSlot("B3");
            Assert.AreEqual("Munch Munch, Yum!", vm.DispenseItem());
            vm.SelectSlot("B4");
            Assert.AreEqual("Munch Munch, Yum!", vm.DispenseItem());
        }
        [TestMethod]
        public void DispenseItem_CRow_GlugGlugYum()
        {
            //Must have enough money to allow dispense function
            vm.AddMoneyToTheVendingMachine(10);
            vm.AddMoneyToTheVendingMachine(10);
            vm.AddMoneyToTheVendingMachine(10);

            vm.SelectSlot("C1");
            Assert.AreEqual("Glug Glug, Yum!", vm.DispenseItem());
            vm.SelectSlot("C2");
            Assert.AreEqual("Glug Glug, Yum!", vm.DispenseItem());
            vm.SelectSlot("C3");
            Assert.AreEqual("Glug Glug, Yum!", vm.DispenseItem());
            vm.SelectSlot("C4");
            Assert.AreEqual("Glug Glug, Yum!", vm.DispenseItem());
        }
        [TestMethod]
        public void DispenseItem_DRow_ChewChewYum()
        {
            //Must have enough money to allow dispense function
            vm.AddMoneyToTheVendingMachine(10);
            vm.AddMoneyToTheVendingMachine(10);
            vm.AddMoneyToTheVendingMachine(10);

            vm.SelectSlot("D1");
            Assert.AreEqual("Chew Chew, Yum!", vm.DispenseItem());
            vm.SelectSlot("D2");
            Assert.AreEqual("Chew Chew, Yum!", vm.DispenseItem());
            vm.SelectSlot("D3");
            Assert.AreEqual("Chew Chew, Yum!", vm.DispenseItem());
            vm.SelectSlot("D4");
            Assert.AreEqual("Chew Chew, Yum!", vm.DispenseItem());
        }
    }
}
