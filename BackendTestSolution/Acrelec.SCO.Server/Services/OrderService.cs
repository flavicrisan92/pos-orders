using Acrelec.SCO.Server.Model.RestExchangedMessages;
using System;

namespace Acrelec.SCO.Server.Services
{
    public class OrderService : IOrdersService
    {
        public string InjectOrder(InjectOrderRequest orderRequest)
        {
            Random rnd = new();
            int orderNumber = rnd.Next();
            return orderNumber.ToString();
        }
    }
}
