using System;
using System.Collections.Generic;
using System.Threading;

namespace Persistence
{
    public class Order
    {

        public Order() { }

        public Order(User user, Item orderItem, List<Item> listItem, int order_id, DateTime order_date)
        {
            this.user = user;
            OrderItem = orderItem;
            ListItem = listItem;
            this.order_id = order_id;
            this.order_date = order_date;
        }
        public User user { get; set; }
        public Item OrderItem { get; set; }
        public List<Item> ListItem { get; set; }
        public int order_id { get; set; }
        public DateTime order_date { get; set; }

    }
}