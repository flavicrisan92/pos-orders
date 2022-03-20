using Acrelec.SCO.Core.Interfaces;
using Acrelec.SCO.DataStructures;
using System.Threading.Tasks;

namespace Acrelec.SCO.Core.Managers
{
    public class OrderManager : IOrderManager
    {
        private IItemsProvider _itemsProvider { get; set; }
        public Customer _customer { get; private set; }

        /// <summary>
        /// constructor
        /// </summary>
        public OrderManager(IItemsProvider itemsProvider, Customer customer)
        {
            _itemsProvider = itemsProvider;
            _customer = customer;
        }

        //todo - implement interface knowing that it has to call the REST API described in readme.txt file 
        public async Task<string> InjectOrderAsync(Order orderToInject)
        {
            return await _itemsProvider.SendOrderAsync(orderToInject, _customer);
        }
    }
}
