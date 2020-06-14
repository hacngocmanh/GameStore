using System;
using MySql.Data.MySqlClient;
using System.IO;
using System.Collections.Generic;
namespace DAL
{
    public class DBHelper
    {

        public static MySqlConnection OpenConnection()
        {
            try
            {
                string connectionString;

                FileStream filestream = File.OpenRead("ConnectionString.txt");
                using (StreamReader reader = new StreamReader(filestream))
                {
                    connectionString = reader.ReadLine();

                }
                filestream.Close();

                return OpenConnection(connectionString);
            }
            catch
            {
                return null;
            }
        }

        public static MySqlConnection OpenConnection(string connectionString)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection
                {
                    ConnectionString = connectionString
                };
                connection.Open();
                return connection;
            }
            catch
            {
                return null;
            }
        }
    }
}
