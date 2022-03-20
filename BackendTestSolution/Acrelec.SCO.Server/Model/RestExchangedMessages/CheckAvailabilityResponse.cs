namespace Acrelec.SCO.Server.Model.RestExchangedMessages
{
    public class CheckAvailabilityResponse
    {
        /// <summary>
        /// if true then Core can inject orders
        /// </summary>
        public bool CanInjectOrders { get; set; }
    }
}
