using Acrelec.SCO.Server.Model.RestExchangedMessages;

namespace Acrelec.SCO.Server.Services
{
    public interface IOrdersService
    {
        string InjectOrder(InjectOrderRequest orderRequest);
    }
}
