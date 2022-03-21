using Acrelec.SCO.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acrelec.SCO.Core.Interfaces
{
    public interface IOrderManager
    {
        /// <summary>
        /// inject an order into the POS system
        /// </summary>
        /// <param name="orderToInject"></param
        /// <param name="customer"></param>
        /// <returns></returns>
        Task<string> InjectOrderAsync(Order orderToInject, Customer customer);
    }
}
