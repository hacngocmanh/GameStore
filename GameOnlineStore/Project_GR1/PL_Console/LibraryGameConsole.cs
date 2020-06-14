using System;
using BL;
using Persistence;
namespace PL_Console {
    public class LibraryGameConsole {
        private ItemBL itemBL = new ItemBL ();
        private OrderBL orderBL = new OrderBL ();
        private static Menu m = new Menu ();
        private static MenuShop ms = new MenuShop ();
        // hiểm thị danh sách trò chơi đã mua của user
        public void LibraryGame (User user) {

            try {
                orderBL.GetAllOrderByUserID (user.user_id);
            } catch (System.Exception) {
                Console.WriteLine ("Mất kết nối dữ liệu!\nNhấn phím bất kì để quay lại trang chủ ");
                Console.ReadKey ();
                m.MainMenu ();
            }

            Console.Clear ();
            Console.WriteLine ("|==================================================================================|");
            Console.WriteLine ("|-----------------------------| TRÒ CHƠI CỦA BẠN |---------------------------------|");
            Console.WriteLine ("|==================================================================================|\n");

            int i = 0;
            // kiểm tra order in user_id
            if (orderBL.GetAllOrderByUserID (user.user_id).Count == 0) {

                 Console.WriteLine ("Bạn chưa có trò chơi nào, vào cửa hàng để mua trò chơi !");
            }
            foreach (Order order in orderBL.LibGame (user.user_id)) {
                i++;
                Console.WriteLine (" {0}. {1}", i, itemBL.GetItemByID (order.OrderItem.item_id).item_name);
            }
            Console.WriteLine ("\n|==================================================================================|");
            Console.WriteLine ("Ấn phím bất kì để quay trở lại");
            Console.ReadLine ();
            ms.menuShop (user);
        }
    }
}