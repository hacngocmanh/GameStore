using System;
using System.Text;
using System.Text.RegularExpressions;
using BL;
using Persistence;

namespace PL_Console {
    public class MenuLogin {

        private UserBL userBL = new UserBL ();
        private User user = new User ();
        private static Menu m = new Menu ();
        private static MenuShop ms = new MenuShop ();
        public void Login () // hiển thị đăng nhập và check user password
        {
            string username = null;
            string password = null;

            while (true) {
                Console.Clear ();
                Console.WriteLine ("|==================================================|");
                Console.WriteLine ("|------------------| ĐĂNG NHẬP |-------------------|");
                Console.WriteLine ("|==================================================|\n");
                Console.Write ("- Tài khoản : ");
                username = Console.ReadLine ();
                Console.Write ("- Mật khẩu  : ");
                password = FormatAndValid.Password ();

                string choice;
                // kiểm tra kí tự name password
                if ((FormatAndValid.validate (username) == false) || (FormatAndValid.validate (password) == false)) {
                    Console.WriteLine ("Tên đăng nhập hoặc mật khẩu không được chứa kí tự đặc biệt!");
                    Console.WriteLine ("Bạn có muốn thử lại không?(C/K): ");
                    choice = Console.ReadLine ().ToUpper ();

                    while (true) {
                        if (choice != "C" && choice != "K") {
                            Console.Write ("Chỉ được nhập C hoặc K: ");
                            choice = Console.ReadLine ().ToUpper ();
                            continue;
                        }
                        break;
                    }

                    switch (choice) {
                        case "c":
                            continue;
                        case "C":
                            continue;
                        case "K":
                            m.MainMenu ();
                            break;
                        case "k":
                            m.MainMenu ();
                            break;
                    }
                }
                // check kết nối user password vs user password sql
                try {
                    user = userBL.GetUser (username, password);
                } catch (System.Exception) {

                    Console.Write ("Mất kết nối, bạn có muốn đăng nhập lại không?(C/K): ");
                    choice = Console.ReadLine ().ToUpper ();
                    while (true) {
                        if (choice != "C" && choice != "K") {
                            Console.Write ("Chỉ được nhập C hoặc K: ");
                            choice = Console.ReadLine ().ToUpper ();
                            continue;
                        }
                        break;
                    }
                    switch (choice) {
                        case "c":
                            continue;
                        case "C":
                            continue;
                        case "K":
                            m.MainMenu ();
                            break;
                        case "k":
                            m.MainMenu ();
                            break;

                    }
                }

                if (user == null) {
                    Console.Write ("Tài khoản hoặc mật khẩu không chính xác!\nBạn có muốn thử lại không?(C/K): ");
                    choice = Console.ReadLine ().ToUpper ();
                    while (true) {
                        if (choice != "C" && choice != "K") {
                            Console.Write ("Chỉ được nhập C hoặc K: ");
                            choice = Console.ReadLine ().ToUpper ();
                            continue;
                        }
                        break;

                    }
                    switch (choice) {
                        case "c":
                            continue;
                        case "C":
                            continue;
                        case "K":
                            m.MainMenu ();
                            break;
                        case "k":
                            m.MainMenu ();
                            break;
                        default:
                            continue;
                    }
                }
                break;
            }
            // nhập đúng user thì chuyển sang trang shop
            if (user != null) {
                ms.menuShop (userBL.GetUserInfo (user.user_id));
            }
        }

    }
}