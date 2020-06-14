using System;
using Persistence;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
namespace DAL
{
    public class UserDAL
    {
        private string query;
        private MySqlDataReader reader;
        private MySqlConnection connection;

        public UserDAL()
        {
            connection = DBHelper.OpenConnection();
        }

        public User GetUser(string username, string password)
        {

            if (connection == null)
            {
                connection = DBHelper.OpenConnection();
            }

            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

            query = @"select * from users where user_name = '" + username + "' and user_password= '" + password + "';";
            MySqlCommand command = new MySqlCommand(query, connection);
            User user = null;
            using (reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    user = GetUser(reader);
                }
            }
            connection.Close();
            return user;
        }
        public bool AddFund(User user, double values)
        {
            if (connection == null)
            {
                connection = DBHelper.OpenConnection();
            }

            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            if(values < 0)
            {
                return false;
            }
            bool result = true;
            
            MySqlCommand command = connection.CreateCommand();
            MySqlTransaction transaction = connection.BeginTransaction();
            
            command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = "lock tables Users write , Orders write, items write, OrderDetail write,feedbackusers write;";
            command.ExecuteNonQuery();
            transaction = connection.BeginTransaction();
            command.Transaction = transaction;
            try
            {
                command.CommandText = @"update users set user_balance = user_balance + " + values + " where user_id = " + user.user_id + ";";
                command.ExecuteNonQuery();
                transaction.Commit();
                result = true;
            }
            catch (System.Exception)
            {

                result = false;
                transaction.Rollback();
            }
            finally
            {
                command.CommandText = "unlock tables;";
                command.ExecuteNonQuery();
                connection.Close();
            }
            return result;

        }
        public User GetUserByID(int userid)
        {
            if (connection == null)
            {
                connection = DBHelper.OpenConnection();
            }

            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            User user = null;
            connection = DBHelper.OpenConnection();
            query = @"select * from users where user_id = " + userid + ";";
            MySqlCommand command = new MySqlCommand(query, connection);
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                user = GetUserInfo(reader);
            }
            connection.Close();
            return user;
        }
        public User GetUser(MySqlDataReader reader)
        {
            User user = new User();
            user.user_id = reader.GetInt32("user_id");
            return user;
        }
        public static User GetUserInfo(MySqlDataReader reader)
        {
            User user = new User();
            user.user_name = reader.GetString("user_name");
            user.password = reader.GetString("user_password");
            user.user_email = reader.GetString("user_email");
            user.user_id = reader.GetInt32("user_id");
            user.user_balance = reader.GetDouble("user_balance");
            user.phone = reader.GetString("user_phone");
            return user;
        }


    }
}