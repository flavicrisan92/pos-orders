using System;
using System.Collections.Generic;
using System.Text;

namespace Acrelec.SCO.DataStructures
{
    public class Order
    {
        /// <summary>
        /// list of all items of an order
        /// </summary>
        public List<OrderedItem> OrderItems { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        public Order()
        {
            OrderItems = new List<OrderedItem>();
        }

        public void AddOrderItems(POSItem pOSItem, int qty)
        {
            var orderedItem = new OrderedItem
            {
                ItemCode = pOSItem.ItemCode,
                Qty = qty
            };
            OrderItems.Add(orderedItem);
        }
    }
}
