using System;
using System.Globalization;
using BL;
using Persistence;

namespace PL_Console {
    public class MenuShop {
        public static int itemid;
        private static Menu m = new Menu ();
        private Order order = new Order ();
        private static UserInfo userinfo = new UserInfo ();
        private Item item = new Item ();
        private UserBL userBL = new UserBL ();
        private ItemBL itemBL = new ItemBL ();
        private static OrderConsole orderConsole = new OrderConsole ();
        private static LibraryGameConsole game = new LibraryGameConsole ();
        private static FeedBackConsole fbc = new FeedBackConsole ();

        public void menuShop (User user) // hiển thị menu shop
        {

            Console.Clear ();
            Console.WriteLine ("|===================================|");
            Console.WriteLine ("|-----------| TRANG CHỦ |-----------|");
            Console.WriteLine ("|===================================|");
            Console.WriteLine ("| 1. Cửa hàng trò chơi              |");
            Console.WriteLine ("| 2. Trò chơi của bạn               |");
            Console.WriteLine ("| 3. Tài khoản                      |");
            Console.WriteLine ("| 4. Giỏ hàng                       |");
            Console.WriteLine ("| 5. Đăng xuất                      |");
            Console.WriteLine ("|===================================|\n");
            int chooseMS;
            while (true) {
                Console.Write (" Chọn: ");
                bool isInt = Int32.TryParse (Console.ReadLine (), out chooseMS);
                if (chooseMS < 1 || chooseMS > 5) {
                    Console.WriteLine ("Nhập giá trị sai !");
                    Console.Write ("Mời bạn nhập lại: ");
                } else if (!isInt) {
                    Console.WriteLine ("Nhập giá trị sai !");
                    Console.Write ("Mời bạn nhập lại: ");
                } else {
                    break;
                }
            }
            switch (chooseMS) {
                case 1:
                    ShowItems (user);
                    break;
                case 2:
                    game.LibraryGame (user);
                    break;
                case 3:
                    userinfo.UserInfoMenu (user);
                    break;
                case 4:
                    orderConsole.ShowCarts (user);
                    break;
                case 5:
                    m.MainMenu ();
                    break;
                default:
                    break;
            }
        }
        public void ShowItems (User user) // hiển thị danh sách trò chơi ở database bên sql
        {

            Console.Clear ();
            try {
                foreach (var item in itemBL.GetAllItems ()) {
                    string format = string.Format ($"{item.item_id,-10}|{item.item_name,-25}|{FormatAndValid.FormatCurrency(item.item_price),-10}");
                }
            } catch (System.Exception) {
                Console.WriteLine ("Mất kết nối dữ liệu!\nBấm phím bất kì để quay lại trang chủ.");
                Console.ReadKey ();
                m.MainMenu ();
            }
            Console.Clear ();
            Console.WriteLine ("|==============================================================|");
            Console.WriteLine ("|--------------------| DANH SÁCH TRÒ CHƠI |--------------------|");
            Console.WriteLine ("|                ----------           ----------               |");
            Console.WriteLine ("|{0,-15}|{1,-25}|{2,-20}|", "Mã trò chơi", "Tên trò chơi", "Giá trò chơi");
            // lấy tất cả item ở sql
            foreach (var item in itemBL.GetAllItems ()) {
                Console.WriteLine ("|--------------------------------------------------------------|");
                string format = string.Format ($"|{item.item_id,-15}|{item.item_name,-25}|{FormatAndValid.FormatCurrency(item.item_price),-20}|");
                Console.WriteLine (format);
            }
            Console.WriteLine ("|--------------------------------------------------------------|\n");
            Console.WriteLine ("1. Chọn sản phẩm     ");
            Console.WriteLine ("2. Quay về Trang chủ ");
            Console.Write ("Chọn: ");
            int chooseItem;
            while (true) {
                bool isINT = Int32.TryParse (Console.ReadLine (), out chooseItem);
                if (!isINT) {
                    Console.WriteLine ("Nhập giá trị sai !");
                    Console.Write ("Mời bạn nhập lại: ");
                } else if (chooseItem < 1 || chooseItem > 2) {
                    Console.WriteLine ("Nhập giá trị sai !");
                    Console.Write ("Mời bạn nhập lại: ");
                } else {
                    break;
                }
            }
            switch (chooseItem) {
                case 1:
                    GetItemInfoByID (user);
                    break;
                case 2:
                    menuShop (user);
                    break;
            }
        }
        public void GetItemInfoByID (User user) // hiển thị chi tiết trò chơi
        {

            Console.WriteLine ("-----------------");
            Console.Write ("Nhập mã trò chơi: ");
            while (true) {
                bool isINT = Int32.TryParse (Console.ReadLine (), out itemid);
                try {
                    item = itemBL.GetItemByID (itemid);
                } catch (System.Exception) {
                    Console.WriteLine ("Mất Kết nối!\nẤn phím bất kì để trở về trang chủ !");
                    Console.ReadKey ();
                    m.MainMenu ();
                }
                if (!isINT) {
                    Console.WriteLine ("Nhập giá trị sai !");
                    Console.Write ("Mời bạn nhập lại: ");
                } else if (itemBL.GetItemByID (itemid) == null) {
                    Console.WriteLine ("Mã game sai hoặc không tồn tại !");
                    Console.Write ("Mời bạn nhập lại: ");
                } else {
                    break;
                }
            }
            Console.Clear ();
            Console.WriteLine ("|===============================================================|");
            Console.WriteLine ("|---------------------| THÔNG TIN TRÒ CHƠI |--------------------|");
            Console.WriteLine ("|===============================================================|");
            Console.WriteLine ($"- Mã trò chơi      : {item.item_id}");
            Console.WriteLine ("----------------------------------------------------------------|");
            Console.WriteLine ($"- Tên trò chơi     : {item.item_name}");
            Console.WriteLine ("|---------------------------------------------------------------|");
            Console.WriteLine ($"- Giá trò chơi     : {FormatAndValid.FormatCurrency(item.item_price)}");
            Console.WriteLine ("|---------------------------------------------------------------|");
            Console.WriteLine ($"- Nhà phát triển   : {item.item_developer}"); 
            Console.WriteLine ("|---------------------------------------------------------------|");   
            Console.WriteLine ($"- Nhà phát hành    : {item.item_publisher}");    
            Console.WriteLine ("|---------------------------------------------------------------|");   
            Console.WriteLine ($"- Thể loại         : {item.item_tag}");    
            Console.WriteLine ("|---------------------------------------------------------------|");   
            Console.WriteLine ($"- Nền tảng         : {item.item_platform}");    
            Console.WriteLine ("|===============================================================|\n");
            Console.WriteLine ($"- Mô tả trò chơi   : {item.item_description}");

            fbc.ShowFeedBacks (user); // hiển thị phản hồi 

            Console.WriteLine ("\n1. Thêm vào giỏ hàng      ");
            Console.WriteLine ("2. Đánh giá trò chơi      ");
            Console.WriteLine ("3. Quay trở lại cửa hàng  ");
            Console.Write ("Chọn: ");
            int chooseAction;
            while (true) {
                bool isINT = Int32.TryParse (Console.ReadLine (), out chooseAction);
                if (!isINT) {
                    Console.WriteLine ("Nhập giá trị sai !");
                    Console.Write ("Mời bạn nhập lại: ");
                } else if (chooseAction < 1 || chooseAction > 3) {
                    Console.WriteLine ("Nhập giá trị sai !");
                    Console.Write ("Mời bạn nhập lại: ");
                } else {
                    break;
                }
            }
            switch (chooseAction) {
                case 1:
                    orderConsole.Order (user);
                    break;
                case 2:
                    fbc.CreateFeedBack (user);
                    break;
                case 3:
                    ShowItems (user);
                    break;
            }
        }
    }
}