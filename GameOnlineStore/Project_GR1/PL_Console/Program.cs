using System;
using BL;
using Persistence;
using MySql.Data.MySqlClient;
using DAL;
namespace PL_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // UserName 1: tk01    || UserName 2: tk02
            // PassWord 1: 123456  || Password 2: 123456
            Menu menu = new Menu();
            menu.MainMenu();
            
        }
    }
}


