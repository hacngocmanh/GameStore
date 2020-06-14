using System;
using Xunit;
using BL;
using Persistence;
using System.IO;
using System.Collections.Generic;

namespace BL.Test
{
    public class OrderBLTest
    {
        private OrderBL orderBL = new OrderBL();
        [Fact]
        public void CreateOrderTestTrue() // check crt order user id vs item id true
        {
             UserBL userbl = new UserBL();
            Order order = new Order();
            order.user = new User();
            order.OrderItem = new Item();
            ItemBL id = new ItemBL();
            order.ListItem  = new List<Item>();
            order.user.user_id = 1;
           
            order.ListItem.Add(id.GetItemByID(101));
            Assert.True(orderBL.CreateOrder(order));
        }
        [Fact]
        public void CreateOrderTestFalse() // check crt order user id vs item id true
        {
             UserBL userbl = new UserBL();
            Order order = new Order();
            order.user = new User();
            order.OrderItem = new Item();
            ItemBL id = new ItemBL();
            order.ListItem  = new List<Item>();
            order.user.user_id = -1;
           
            order.ListItem.Add(id.GetItemByID(-101));
            Assert.False(orderBL.CreateOrder(order));
        }
        [Fact]
        public void GetAllOrderByUserIDTestTrue()
        {
            Assert.NotEmpty(orderBL.GetAllOrderByUserID(1));
        }
        [Fact]
        public void GetAllOrderByUserIDTestFalse()
        {
            Assert.Empty(orderBL.GetAllOrderByUserID(-101));
        }

    }
}
