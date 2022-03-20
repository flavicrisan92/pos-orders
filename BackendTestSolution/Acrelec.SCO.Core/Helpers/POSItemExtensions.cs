using Acrelec.SCO.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acrelec.SCO.Core.Helpers
{
    public static class POSItemExtensions
    {
        //todo - write an extension method which returns only the first 3 letters of the POSItem.Name
        /// <summary>
        /// Extension returning the first 3 letters of the POSItem.Name representing the POSItem code 
        /// </summary>
        /// <param name="posItem">POS item</param>
        /// <returns>Returns the first 3 letters of the POSItem Name property</returns>
        public static string GetShortCode(this POSItem posItem)
        {
            return new string(posItem.Name.Take(3).ToArray());
        }

        /// <summary>
        /// Order all the items by unit price ascending
        /// </summary>
        /// <param name="posItems"></param>
        /// <returns>Returns the ordered list of items by price</returns>
        public static List<POSItem> SortByPriceAscending(this List<POSItem> posItems)
        {
            return posItems.OrderBy(q => q.UnitPrice).ToList();
        }
    }
}
