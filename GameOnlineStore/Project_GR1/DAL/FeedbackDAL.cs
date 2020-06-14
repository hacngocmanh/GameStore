using System;
using Persistence;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DAL
{
    public class FeedBackDAL
    {
        private MySqlConnection connection;
        private MySqlDataReader reader;
        private string query;
        public FeedBackDAL()
        {
            connection = DBHelper.OpenConnection();
        }

        public List<FeedBack> GetFeedBackByItemID(int Itemid)
        {
            if (connection == null)
            {
                connection = DBHelper.OpenConnection();
            }

            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            List<FeedBack> Listfeedback = null;
            connection = DBHelper.OpenConnection();
            query = @"select * from feedbackusers where item_id = " + Itemid + ";";
            MySqlCommand command = new MySqlCommand(query, connection);
            using (reader = command.ExecuteReader())
            {
                Listfeedback = new List<FeedBack>();
                while (reader.Read())
                {
                    Listfeedback.Add(GetFeedBack(reader));
                }
            }
            connection.Close();
            return Listfeedback;
        }
        public FeedBack GetFeedBackForCheck(User user, int item_id)
        {
            if (connection == null)
            {
                connection = DBHelper.OpenConnection();
            }

            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            FeedBack feedback = new FeedBack();
            connection = DBHelper.OpenConnection();
            query = @"select * from feedbackusers where item_id = " + item_id + " and user_id = " + user.user_id + ";";
            MySqlCommand command = new MySqlCommand(query, connection);

            using(reader = command.ExecuteReader())
            {
                if(reader.Read())
                {
                    feedback = GetFeedBack(reader);
                }
            }
            
            connection.Close();
            Console.WriteLine(feedback.Feedback_id);
            return feedback;
            

        }
        public bool UpdateFeedBack(FeedBack feedback)
        {
            if (connection == null)
            {
                connection = DBHelper.OpenConnection();
            }

            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            MySqlCommand command = connection.CreateCommand();
            MySqlTransaction transaction = connection.BeginTransaction();
            bool result = true;
            command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = "lock tables Users write , Orders write, items write, OrderDetail write,feedbackusers write;";
            command.ExecuteNonQuery();
            transaction = connection.BeginTransaction();
            command.Transaction = transaction;

            try
            {
                command.CommandText = " update feedbackusers set FeedBack = '" + feedback.Comment + "',rate = " + feedback.Rate + "  where user_id = " + feedback.user.user_id + " and item_id = " + feedback.item.item_id + ";";
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
                command.CommandText = "unlock tables";
                command.ExecuteNonQuery();
                connection.Close();
            }
            return result;
        }
        public bool CreateFeedback(FeedBack Feedback)
        {
            if (connection == null)
            {
                connection = DBHelper.OpenConnection();
            }

            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            MySqlCommand command = connection.CreateCommand();
            MySqlTransaction transaction = connection.BeginTransaction();
            bool result = true;
            command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = "lock tables Users write , Orders write, items write, OrderDetail write,feedbackusers write;";
            command.ExecuteNonQuery();
            transaction = connection.BeginTransaction();
            command.Transaction = transaction;
            try
            {
                command.CommandText = "insert into FeedBackUsers (user_ID, Item_ID,Rate,FeedBack) values (" + Feedback.user.user_id + "," + Feedback.item.item_id + "," + Feedback.Rate + ",'" + Feedback.Comment + "');";
                command.ExecuteNonQuery();
                command.CommandText = "select LAST_INSERT_ID() as feedback_id;";
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

        public FeedBack GetFeedBack(MySqlDataReader reader)
        {
            FeedBack feedback = new FeedBack();
            feedback.user = new User();
            feedback.item = new Item();
            feedback.Feedback_id = reader.GetInt32("Feedback_id");
            feedback.Rate = reader.GetInt32("Rate");
            feedback.user.user_id = reader.GetInt32("user_id");
            feedback.item.item_id = reader.GetInt32("item_id");
            feedback.Comment = reader.GetString("feedback");
            return feedback;
        }
    }
}