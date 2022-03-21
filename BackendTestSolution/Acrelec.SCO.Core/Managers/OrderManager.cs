using Acrelec.SCO.Core.Interfaces;
using Acrelec.SCO.DataStructures;
using System;
using System.Threading.Tasks;

namespace Acrelec.SCO.Core.Managers
{
    public class OrderManager : IOrderManager
    {
        private IItemsProvider _itemsProvider { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        public OrderManager(IItemsProvider itemsProvider)
        {
            _itemsProvider = itemsProvider;
        }

        //todo - implement interface knowing that it has to call the REST API described in readme.txt file 
        public async Task<string> InjectOrderAsync(Order orderToInject, Customer customer)
        {
            try
            {
                return await _itemsProvider.SendOrderAsync(orderToInject, customer);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }
    }
}
