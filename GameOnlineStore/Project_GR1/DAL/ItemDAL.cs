using System;
using MySql.Data.MySqlClient;
using Persistence;
using System.Collections.Generic;
namespace DAL
{
    public class ItemDAL
    {
        private string query;
        private MySqlDataReader reader;
        private MySqlConnection connection;


        public ItemDAL()
        {
            connection = DBHelper.OpenConnection();
        }

        public Item GetItemByID(int? item_id)
        {
            if(item_id == null)
            {
                return null;
            }
            if(connection == null)
            {
                connection = DBHelper.OpenConnection();
            }
            if(connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            query = $"select * from items where item_id ="+ item_id +";";
            MySqlCommand command = new MySqlCommand(query,connection);
            Item item = null;
            using(reader = command.ExecuteReader())
            {
                if(reader.Read())
                {
                    item = GetItem(reader);
                }
            }
            connection.Close();

            return item;
        }
        
        public List<Item> GetAllItems()
        {
            if(connection == null)
            {
                connection = DBHelper.OpenConnection();

            }
            if(connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

            query = $"select * from items;";

            MySqlCommand command = new MySqlCommand(query,connection);
            List<Item> item = null;

            using(reader = command.ExecuteReader())
            {
                item = new List<Item>();
                while(reader.Read())
                {
                    item.Add(GetItem(reader));
                }
            }
            connection.Close();
            return item;
        }
        public Item GetItem(MySqlDataReader reader)
        {
            Item item = new Item();
            item.item_id = reader.GetInt32("item_id");
            item.item_name = reader.GetString("item_name");
            item.item_price = reader.GetDouble("item_price");
            item.item_description = reader.GetString("item_description");
            item.item_tag = reader.GetString("item_tag");
            item.item_developer = reader.GetString("item_developer");
            item.item_publisher = reader.GetString("item_publisher");
            item.item_platform = reader.GetString("item_platform");
            return item;
        }

    }
}