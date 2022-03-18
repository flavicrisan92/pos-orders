using Acrelec.SCO.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acrelec.SCO.Core.Model.RestExchangedMessages
{
    public class InjectOrderRequest
    {
        public Order Order { get; set; }
        public Customer Customer { get; set; }
    }
}
