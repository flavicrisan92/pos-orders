using Acrelec.SCO.DataStructures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acrelec.SCO.Core.Interfaces
{
    public interface IItemsProvider
    {
        /// <summary>
        /// list of all POS items
        /// </summary>
        List<POSItem> AllPOSItems { get; }

        /// <summary>
        /// list of all POS items that are available
        /// </summary>
        List<POSItem> AvailablePOSItems { get; }

        /// <summary>
        /// check POS availability
        /// </summary>
        /// <returns>POS status</returns>
        Task<bool> CheckServerAvailabilityAsync();

        /// <summary>
        /// Send order to POS
        /// </summary>
        /// <param name="order">Order object</param>
        /// <param name="customer">Customer object</param>
        /// <returns>Order number</returns>
        Task<string> SendOrderAsync(Order order, Customer customer);
    }
}
