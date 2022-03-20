using Acrelec.SCO.Server.Model.RestExchangedMessages;

namespace Acrelec.SCO.Server.Interfaces
{
    public interface IOrdersService
    {
        string InjectOrder(InjectOrderRequest orderRequest);
    }
}
