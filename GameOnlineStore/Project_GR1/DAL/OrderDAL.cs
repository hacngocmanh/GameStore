using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using Persistence;


namespace DAL
{
    public class OrderDAL
    {
        private MySqlDataReader reader;
        private MySqlConnection connection;
        private string query;
        public OrderDAL()
        {
            connection = DBHelper.OpenConnection();
        }

        public List<Order> GetAllOrdersByUserID(int user_id)
        {
            if (connection == null)
            {
                connection = DBHelper.OpenConnection();
            }
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            query = @"select orders.order_id,user_id,item_id,order_date from orders inner join orderdetail on orderdetail.order_id = orders.order_id where user_id = " + user_id + " group by order_id;";

            MySqlCommand command = new MySqlCommand(query, connection);
            List<Order> ListOrder = null;

            using (reader = command.ExecuteReader())
            {
                ListOrder = new List<Order>();
                while (reader.Read())
                {
                    ListOrder.Add(GetOrder(reader));
                }
            }
            connection.Close();
            return ListOrder;
        }
        public List<Order> LibGame(int user_id)
        {
            if (connection == null)
            {
                connection = DBHelper.OpenConnection();
            }
            
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            query = @"select orders.order_id,user_id,item_id,order_date from orders inner join orderdetail on orderdetail.order_id = orders.order_id where user_id = " + user_id + ";";

            MySqlCommand command = new MySqlCommand(query, connection);
            List<Order> ListOrder = null;
            
            using (reader = command.ExecuteReader())
            {
                ListOrder = new List<Order>();
                while (reader.Read())
                {
                    ListOrder.Add(GetOrder(reader));
                }
            }
            connection.Close();
            return ListOrder;
        }

        public List<Order> GetAllOrdersByOrderID(int order_id)
        {
            if (connection == null)
            {
                connection = DBHelper.OpenConnection();
            }
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            query = @"select * from orderdetail where order_id = " + order_id + ";";

            MySqlCommand command = new MySqlCommand(query, connection);
            List<Order> ListOrder = null;

            using (reader = command.ExecuteReader())
            {
                ListOrder = new List<Order>();
                while (reader.Read())
                {
                    ListOrder.Add(GetOrder1(reader));
                }
            }
            connection.Close();
            return ListOrder;
        }

        public Order GetOrder(MySqlDataReader reader)
        {
            Order order = new Order();
            order.user = new User();
            order.OrderItem = new Item();
            order.order_id = reader.GetInt32("order_id");
            order.user.user_id = reader.GetInt32("user_id");
            order.order_date = reader.GetDateTime("order_date");
            order.OrderItem.item_id = reader.GetInt32("item_id");

            return order;
        }
        public Order GetOrder1(MySqlDataReader reader)
        {
            Order order = new Order();
            order.OrderItem = new Item();
            order.order_id = reader.GetInt32("order_id");
            order.OrderItem.item_id = reader.GetInt32("item_id");
            return order;
        }
        public bool CreateOrder(Order order)
        {
            bool result = true;
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

            MySqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            // khoa cac bang k cho phep nguoi dung sua
            command.CommandText = "lock tables Users write , Orders write, items write, OrderDetail write,feedbackusers write;";
            command.ExecuteNonQuery();
            MySqlTransaction transaction = connection.BeginTransaction();
            command.Transaction = transaction;
            //MySqlDataReader reader = null;
            try
            {
                //insert order
                command.CommandText = "insert into orders(user_id) values(" + order.user.user_id + ");";
                command.ExecuteNonQuery();
                command.CommandText = "select LAST_INSERT_ID() as order_id";
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    order.order_id = reader.GetInt32("order_id");
                }
                reader.Close();
                foreach (var item in order.ListItem)
                {
                    //insert to orderdetail
                    command.CommandText = "insert into OrderDetail(order_id,item_id) values(" + order.order_id + "," + item.item_id + ");";
                    command.ExecuteNonQuery();
                }

                //update user_balance
                command.CommandText = "update users set user_balance =" + order.user.user_balance + " where user_id = " + order.user.user_id + ";";
                command.ExecuteNonQuery();

                transaction.Commit();
                result = true;

            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
                result = false;
                transaction.Rollback();

            }
            finally
            {
                // mo lai tat ca cac bang
                command.CommandText = "unlock tables;";
                command.ExecuteNonQuery();
                connection.Close();
            }

            return result;
        }
    }
}