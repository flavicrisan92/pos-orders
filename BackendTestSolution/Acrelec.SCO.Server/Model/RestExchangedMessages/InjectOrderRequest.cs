using Acrelec.SCO.DataStructures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acrelec.SCO.Server.Model.RestExchangedMessages
{
    public class InjectOrderRequest
    {
        public Order Order { get; set; }

        public Customer Customer { get; set; }
    }
}
