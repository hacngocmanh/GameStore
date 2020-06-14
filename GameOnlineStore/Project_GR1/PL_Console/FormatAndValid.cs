using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace PL_Console
{
    public class FormatAndValid
    {

        // Format money VND
        public static string FormatCurrency(double price)
        {
            string a = string.Format(new CultureInfo("vi-VN"), "{0:#,##0} VND", price);
            return a;
        }

        // Check regex kí tự
        public static bool validate(string str)
        {
            Regex regex = new Regex("[a-zA-Z0-9_]");
            MatchCollection matchColection = regex.Matches(str);
            if (matchColection.Count < str.Length)
            {
                return false;
            }
            return true;
        }

        //Check nhập **** cho password
        public static string Password(char mask = '*')
        {
            var sb = new StringBuilder();
            ConsoleKeyInfo keyInfo;
            while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Enter)
            {
                if (!char.IsControl(keyInfo.KeyChar))
                {
                    sb.Append(keyInfo.KeyChar);
                    Console.Write(mask);
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && sb.Length > 0)
                {
                    sb.Remove(sb.Length - 1, 1);

                    if (Console.CursorLeft == 0)
                    {
                        Console.SetCursorPosition(Console.BufferWidth - 1, Console.CursorTop - 1);
                        Console.Write(' ');
                        Console.SetCursorPosition(Console.BufferWidth - 1, Console.CursorTop - 1);
                    }
                    else Console.Write("\b \b");
                }
            }
            Console.WriteLine();
            return sb.ToString();
        }
    }

}



