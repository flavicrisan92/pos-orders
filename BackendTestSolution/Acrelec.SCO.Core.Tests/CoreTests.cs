using Acrelec.SCO.Core.Interfaces;
using Acrelec.SCO.Core.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Acrelec.SCO.Core.Tests
{
    [TestClass]
    public class CoreTests
    {
        [TestMethod]
        public void ItemsProviderTest()
        {
            IItemsProvider itemsProvider = new ItemsProvider();
            Assert.AreEqual(4, itemsProvider.AllPOSItems.Count, "Different number of items are expected");

            //todo - write an assert to check only for items that are available (IsAvailable=True)
        }

        [TestMethod]
        public void OrderedItemsByCodeTest()
        {
            IItemsProvider itemsProvider = new ItemsProvider();
            string[] expectedCodesOrder = new[] { "200", "100", "101", "50" };

            //todo - write the code to order the items ascendent by UnitPrice
            string[] orderedCodes = new[] { "implement this" };

            //compare the ordered itemCodes to see it matches the expected order
            Assert.AreEqual(expectedCodesOrder, orderedCodes);
        }
    }
}
