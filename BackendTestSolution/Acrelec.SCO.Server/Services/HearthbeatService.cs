using Acrelec.SCO.Server.Interfaces;
using Acrelec.SCO.Server.Model.RestExchangedMessages;

namespace Acrelec.SCO.Server.Services
{
    public class HearthbeatService : IHearthbeatService
    {
        public CheckAvailabilityResponse CheckDependencies()
        {
            return new CheckAvailabilityResponse(true);
        }
    }
}
