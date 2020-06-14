using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using BL;
using Persistence;

namespace PL_Console
{
    public class UserInfo
    {
        private UserBL userBL = new UserBL();
        private PurchaseConsole p = new PurchaseConsole();
        private User user = new User();
        private static Menu m = new Menu();
        private static MenuShop ms = new MenuShop();

        public void UserInfoMenu(User user) // hiển thị menu thông tin khách hàng
        {
            Console.Clear();
            Console.WriteLine("|========================================|");
            Console.WriteLine("|--------| THÔNG TIN KHÁCH HÀNG |--------|");
            Console.WriteLine("|========================================|");
            Console.WriteLine("| 1. Thông tin tài khoản                 |");
            Console.WriteLine("| 2. Lịch sử giao dịch                   |");
            Console.WriteLine("| 3. Nạp tiền                            |");
            Console.WriteLine("| 4. Trở về trang chủ                    |");
            Console.WriteLine("|========================================|");
            int chooseUI;
            while (true)
            {
                Console.Write(" Chọn: ");
                bool isInt = Int32.TryParse(Console.ReadLine(), out chooseUI);
                if (chooseUI < 1 || chooseUI > 4)
                {
                    Console.WriteLine("Bạn đã nhập sai! Hãy nhập lại");
                }
                else if (!isInt)
                {
                    Console.WriteLine("Bạn đã nhập sai! Hãy nhập lại");
                }
                else
                {
                    break;
                }
            }
            switch (chooseUI)
            {
                case 1:
                    GetUserInfo(user.user_id);
                    break;
                case 2:
                    p.GetAllOrdersByUserID(user);
                    break;
                case 3:
                    AddFund(user);
                    break;
                case 4:
                    ms.menuShop(user);
                    break;
            }
        }
        public void AddFund(User user) // nạp tiền vào tài khoản
        {
            Console.Clear();
            double values;
            string password;
            Console.WriteLine("|========================================|");
            Console.WriteLine("|-------| NẠP TIỀN VÀO TÀI KHOẢN |-------|");
            Console.WriteLine("|========================================|\n");
            Console.Write("Nhập số tiền cần nạp: ");

            while (true)
            {
                bool isDouble = Double.TryParse(Console.ReadLine(), out values);
                if (!isDouble)
                {
                    Console.Write("Bạn đã nhập sai!\nHãy nhập lại: ");
                }
                else if (values > 9999999999 || values <= 0)
                {
                    Console.WriteLine("Số tiền vượt quá giới hạn (9.999.999.999 VND/lần) hoặc (số tiền <= 0 VND )!");
                    Console.Write("Hãy nhập lại: ");
                }
                else
                {
                    break;
                }
            }
            Console.Write("Nhập lại mật khẩu để xác nhận giao dịch: ");
            password = FormatAndValid.Password(); // check pass đúng thì thành công else thất bại

            if (userBL.GetUser(user.user_name, password) != null)
            {
                userBL.Addfund(user, values);
                Console.WriteLine("GIAO DỊCH THÀNH CÔNG !\nBấm phím bất kì để tiếp tục !");
                Console.ReadKey();
                GetUserInfo(user.user_id);
            }
            else
            {
                Console.WriteLine("GIAO DỊCH KHÔNG THÀNH CÔNG!\nHãy chắc chắn bạn đã nhập đúng mật khẩu !");
                Console.Write("Bạn có muốn thực hiện lại giao dịch không ?(C/K): ");
                string choose;
                while (true)
                {
                    choose = Console.ReadLine().ToUpper();
                    if (choose != "C" && choose != "K")
                    {
                        Console.WriteLine("Bạn đã nhập sai, hãy nhập lại!   ");
                        Console.Write("Bạn có muốn tiếp tục không(C/K)?: ");

                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                switch (choose)
                {
                    case "C":
                        AddFund(user);
                        break;
                    case "K":
                        UserInfoMenu(user);
                        break;
                }
            }

        }
        public void GetUserInfo(int userid) // hiển thị thông tin tài khoản
        {
            Console.Clear();
            
            user.user_id = userid;
            try
            {
                user = userBL.GetUserInfo(userid);
            }
            catch (System.Exception)
            {
                Console.WriteLine("Mất kết nối dữ liệu!");
                Console.WriteLine("Ấn phím bất kì để quay trở lại Trang chủ.");
                Console.ReadKey();
                m.MainMenu();
            }

            Console.WriteLine("|=================================================================|");
            Console.WriteLine("|--------------------| THÔNG TIN TÀI KHOẢN |----------------------|");
            Console.WriteLine("|=================================================================|");
            Console.WriteLine($"- Tên đăng nhập    : {user.user_name}");
            Console.WriteLine($"- Mã tài khoản     : {user.user_id}");
            Console.WriteLine($"- Email            : {user.user_email}");
            Console.WriteLine($"- Số điện thoại    : {user.phone}");
            Console.WriteLine($"- Số dư tài khoản  : {FormatAndValid.FormatCurrency(user.user_balance)}");
            Console.WriteLine("|=================================================================|\n");
            Console.WriteLine("Bấm phím bất kì để quay lại!");
            Console.ReadKey();
            UserInfoMenu(user);
        }

    }
}