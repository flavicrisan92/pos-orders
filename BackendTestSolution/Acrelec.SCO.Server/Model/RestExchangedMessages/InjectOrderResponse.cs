using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acrelec.SCO.Server.Model.RestExchangedMessages
{
    public class InjectOrderResponse
    {
        /// <summary>
        /// order number assigned by the POS system
        /// </summary>
        public string OrderNumber { get; set; }
    }
}
