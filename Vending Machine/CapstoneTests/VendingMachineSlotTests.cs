using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineSlotTests
    {
        VendingMachineSlot vms;
        VendingMachineItem bubbleGum;
        VendingMachineItem candyBar;

        [TestInitialize]
        public void Initialize()
        {
            vms = new VendingMachineSlot();
            bubbleGum = new VendingMachineItem("Bubble Gum", 2.00M);
            candyBar = new VendingMachineItem("Candy Bar", 2.50M);
        }

        [TestMethod]
        public void PlaceItemInSlot_NewItem_TrueAndQTY5()
        {
            Assert.AreEqual(true, vms.PlaceItemInSlot(bubbleGum));
            Assert.AreEqual(5, vms.QuantityOfItemInSlot);
        }

        [TestMethod]
        public void PlaceItemInSlot_OverExistingItem_False()
        {
            vms.PlaceItemInSlot(bubbleGum);
            Assert.AreEqual(false, vms.PlaceItemInSlot(candyBar));
            Assert.AreEqual(false, vms.PlaceItemInSlot(bubbleGum));
        }

        [TestMethod]
        public void TakeItemFromSlot_ItemIsAvailable_TrueAndQTYUpdated()
        {
            vms.PlaceItemInSlot(bubbleGum);
            Assert.AreEqual(true, vms.TakeItemFromSlot());
            Assert.AreEqual(4, vms.QuantityOfItemInSlot);
        }

        [TestMethod]
        public void IsEmpty_NoItemsLeft_True()
        {
            vms.PlaceItemInSlot(bubbleGum);
            vms.TakeItemFromSlot();
            vms.TakeItemFromSlot();
            vms.TakeItemFromSlot();
            vms.TakeItemFromSlot();
            vms.TakeItemFromSlot();
            Assert.AreEqual(true, vms.IsEmpty);
        }

        [TestMethod]
        public void IsEmpty_ItemsLeft_False()
        {
            vms.PlaceItemInSlot(bubbleGum);
            Assert.AreEqual(false, vms.IsEmpty);
        }
    }
}



