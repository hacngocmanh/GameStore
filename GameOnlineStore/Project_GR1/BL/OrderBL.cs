using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DAL;
using Persistence;
using System.Linq;

namespace BL
{
    public class OrderBL
    {
        private OrderDAL orderDAL = new OrderDAL();
        private ItemDAL itemDAL = new ItemDAL();

        public bool CreateOrder(Order Order) 
        {
            bool result = orderDAL.CreateOrder(Order);
            return result;
        }
        public List<Order> GetAllOrderByOrderID(int order_id)
        {
            return orderDAL.GetAllOrdersByOrderID(order_id);
        }
        public List<Order> LibGame(int user_id)
        {
            return orderDAL.LibGame(user_id);
        }
        public List<Order> GetAllOrderByUserID(int user_id)
        {
            return orderDAL.GetAllOrdersByUserID(user_id);
        }

    }

}