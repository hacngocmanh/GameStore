using System;
using Xunit;
using DAL;
using Persistence;
using System.IO;
using System.Collections.Generic;

namespace DAL.Test
{
    public class OrderDALTest
    {
        private OrderDAL orderDal = new OrderDAL();
        [Fact]
        public void CreateOrderTestTrue() // check tao order user id vs item id true
        {
            UserDAL userdal = new UserDAL();
            Order order = new Order();
            order.user = new User();
            order.OrderItem = new Item();
            ItemDAL id = new ItemDAL();
            order.ListItem  = new List<Item>();
            order.user.user_id = 1;
           
            order.ListItem.Add(id.GetItemByID(101));
            Assert.True(orderDal.CreateOrder(order));
        }
        [Fact]
        public void CreateOrderTestFalse() // check tao order user id vs item id Dalse
        {
            UserDAL userdal = new UserDAL();
            Order order = new Order();
            order.user = new User();
            order.OrderItem = new Item();
            ItemDAL id = new ItemDAL();
            order.ListItem  = new List<Item>();
            order.user.user_id = -1;
           
            order.ListItem.Add(id.GetItemByID(-101));
            Assert.False(orderDal.CreateOrder(order));
        }
    
        [Fact]
        public void GetAllOrdersByUserIDTestTrue()
        {
            Assert.NotEmpty(orderDal.GetAllOrdersByUserID(1));
        }

        [Fact]
        public void GetAllOrdersByUserIDTestFalse()
        {
            Assert.Empty(orderDal.GetAllOrdersByUserID(-101));
        }
    }
}
