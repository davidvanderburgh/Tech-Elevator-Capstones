using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineItemTests
    {
        VendingMachineItem vmi;

        [TestInitialize]
        public void Initialize()
        {
            vmi = new VendingMachineItem("Bubble Gum", 2.00M);
        }

        [TestMethod]
        public void VendingMachineItemPriceSet_PositiveValue_Value()
        {
            vmi.Price = 10.0M;
            Assert.AreEqual(10.0M, vmi.Price);

            vmi.Price = 0.0M;
            Assert.AreEqual(0.0M, vmi.Price);
        }
    }
}
