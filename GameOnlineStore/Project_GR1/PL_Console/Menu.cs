using System;

namespace PL_Console
{
    public class Menu
    {
        private static MenuLogin mlg = new MenuLogin();
        public void MainMenu()// hiển thị menu chính và đăng nhập
        {
            Console.Clear();
            
            Console.WriteLine("|=============================================================|");
            Console.WriteLine("|========| CHÀO MỪNG BẠN ĐẾN VỚI ONLINE-GAME-STORE |==========|");
            Console.WriteLine("|=============================================================|");
            Console.WriteLine("|  1. Đăng nhập                                               |");
            Console.WriteLine("|  0. Thoát                                                   |");   
            Console.WriteLine("|=============================================================|");

            int chooseMainMenu;
            while (true)
            {
                Console.Write(" Chọn: ");
                bool isInt = Int32.TryParse(Console.ReadLine(), out chooseMainMenu);
                if (chooseMainMenu < 0 || chooseMainMenu > 1)
                {
                    Console.WriteLine("Bạn đã chọn sai, hãy chọn lại!");
                }
                else if (!isInt)
                {
                    Console.WriteLine("Bạn đã nhập sai, hãy nhập lại!");
                }
                else
                {
                    break;
                }
            }
            switch (chooseMainMenu)
            {
                case 1: //chuyển sang đăng nhập
                    mlg.Login();
                    break;
                case 0: // thoát
                    Environment.Exit(0);
                    break;
            }
        }
    }
}