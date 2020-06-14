using System;
using System.Collections.Generic;
using System.IO;
using BL;
using Newtonsoft.Json;
using Persistence;
namespace PL_Console {
    public class PurchaseConsole {
        private OrderBL orderBL = new OrderBL ();
        private ItemBL itemBL = new ItemBL ();
        private UserBL userBL = new UserBL ();
        private static MenuShop ms = new MenuShop ();
        private static Menu m = new Menu ();
        private static UserInfo u = new UserInfo ();
        private static OrderConsole orderConsole = new OrderConsole ();

        public void Purchase (User user) // thanh toán sản phẩm
        {
            Order order = new Order ();
            order.ListItem = new List<Item> ();
            Console.Write ("Bạn có muốn thanh toán không ?(C/K): ");
            string choose1;
            while (true) {
                choose1 = Console.ReadLine ().ToUpper ();
                if (choose1 != "C" && choose1 != "K") {
                    Console.WriteLine ("Bạn đã nhập sai, hãy nhập lại!   ");
                    Console.WriteLine ("Bạn có muốn thanh toán không ?(C/K): ");
                    continue;
                } else {
                    break;
                }
            }
            switch (choose1) {
                case "C":
                    break;
                case "K":
                    orderConsole.ShowCarts (user);
                    break;
            }
            Console.Clear ();
            try {
                StreamReader r = new StreamReader ("order" + user.user_id + ".json");
                r.Close ();
            } catch (System.Exception) {
                Console.WriteLine ("Mất kết nối dữ liệu!");
                Console.Write ("Bấm phím bất kì để tiếp tục ");
                Console.ReadKey ();
                m.MainMenu ();
            }
            using (StreamReader r = new StreamReader ("order" + user.user_id + ".json")) {
                double price = 0;
                var json = r.ReadToEnd ();
                r.Close ();
                var ListOrder = JsonConvert.DeserializeObject<Order> (json);
                Console.Clear ();
                Console.WriteLine ("|================================================|");
                Console.WriteLine ("|---------------| CHI TIẾT ĐƠN HÀNG |------------|");
                Console.WriteLine ("|================================================|");
                Console.WriteLine ("|{0,-25}|{1,-20}  |", "Tên sản phẩm", "Giá sản phẩm");
                Console.WriteLine ("|------------------------------------------------|");
                foreach (var orders in ListOrder.ListItem) {
                    string format = string.Format ($"|{orders.item_name,-25}|{FormatAndValid.FormatCurrency(orders.item_price),-15}       |");
                    Console.WriteLine (format);
                    Console.WriteLine ("|================================================|");
                    price += orders.item_price; // tính tổng giá của tất cả sản phẩm có trong giỏ hàng
                    if (user.user_balance < price) // nếu tiền ko đủ thì nạp hoặc trở lại
                    {
                        Console.WriteLine ("Tài khoản của quý khách không đủ để thực hiện giao dịch này !");
                        Console.Write ("Bạn có muốn nạp thêm tiền vào tài khoản không?(C/K): ");
                        string choose;
                        while (true) {
                            choose = Console.ReadLine ().ToUpper ();
                            if (choose != "C" && choose != "K") {
                                Console.WriteLine ("Bạn đã nhập sai, hãy nhập lại!");
                                Console.Write ("Bạn có muốn nạp thêm tiền vào tài khoản không?(C/K)?: ");
                                continue;
                            } else {
                                break;
                            }
                        }
                        switch (choose) {
                            case "C":
                                u.AddFund (user);
                                break;
                            case "K":
                                orderConsole.ShowCarts (user);
                                break;
                        }
                    } else // trừ tiền trong bảng order
                    {
                        user.user_balance = user.user_balance - price;
                    }

                    order.ListItem.Add (orders); // thêm order vào list
                }
                order.user = user;
                order.order_id = ListOrder.order_id;
            }
            if (orderBL.CreateOrder (order) == true) // nếu mua hàng thành công thì xóa file chưa thì vẫn lưu trong file
            {
                Console.WriteLine ("Mua hàng thành công!\nẤn phím bất kì để tiếp tục! ");
                File.Delete ("order" + user.user_id + ".json");
                Console.ReadKey ();
                ms.menuShop (user);
            } else {
                Console.WriteLine ("Mua hàng Không thành công, đã có lỗi xảy ra \nẤn phím bất kì để quay về Trang chủ!");
                Console.ReadKey ();
                ms.menuShop (user);
            }
        }
        public void GetAllOrdersByUserID (User user) // chi tiết lịch sử giao dịch 
        {
            Console.Clear ();

            try {
                orderBL.GetAllOrderByUserID (user.user_id);
            } catch (System.Exception) {
                Console.WriteLine ("Mất kết nối dữ liệu!\nNhấn phím bất kì để quay lại Trang chủ ");
                Console.ReadKey ();
                m.MainMenu ();
            }
            if (orderBL.GetAllOrderByUserID (user.user_id).Count == 0) {
                Console.WriteLine ("|=========================================|");
                Console.WriteLine ("|---------| LỊCH SỬ GIAO DỊCH |-----------|");
                Console.WriteLine ("|=========================================|");
                Console.WriteLine ("|{0,-15}|{1,-25}|", "Mã đơn hàng", "Ngày thanh toán");
                Console.WriteLine ("\nBạn chưa mua sản phẩm nào từ cửa hàng !\n");
                Console.WriteLine ("|=========================================|");
                Console.WriteLine ("Ấn phím kì để quay cửa hàng");
                Console.ReadKey ();
                u.UserInfoMenu (user);
            }

            Console.WriteLine ("|=========================================|");
            Console.WriteLine ("|---------| LỊCH SỬ GIAO DỊCH |-----------|");
            Console.WriteLine ("|=========================================|");
            Console.WriteLine ("|{0,-15}|{1,-25}|", "Mã đơn hàng", "Ngày thanh toán");
            foreach (Order order in orderBL.GetAllOrderByUserID (user.user_id)) {
                Console.WriteLine ("|-----------------------------------------|");
                string format = string.Format ($"|{order.order_id,-15}|{order.order_date,-25}|");
                Console.WriteLine (format);
            }
            Console.WriteLine ("|=========================================|");
            Console.WriteLine ("\n1. Xem chi tiết đơn hàng");
            Console.WriteLine ("2. Quay lại trang chủ");
            Console.Write ("Chọn: ");
            int choose;
            while (true) {
                bool isINT = Int32.TryParse (Console.ReadLine (), out choose);
                if (!isINT) {
                    Console.WriteLine ("Nhập giá trị sai !");
                    Console.Write ("Mời bạn nhập lại: ");
                } else if (choose < 1 || choose > 2) {
                    Console.WriteLine ("Nhập giá trị sai !");
                    Console.Write ("Mời bạn nhập lại: ");
                } else {
                    break;
                }
            }
            switch (choose) {
                case 1:
                    OrderDetail (user);
                    break;
                    case 2:
                        ms.menuShop(user);
                        break;
            }
        }
        public void OrderDetail (User user) {
            int order_id;
            Console.Write ("Nhập mã đơn hàng: ");
            while (true) {
                bool isINT = Int32.TryParse (Console.ReadLine (), out order_id);
                if (!isINT) {
                    Console.WriteLine ("Nhập giá trị sai !");
                    Console.Write ("Mời bạn nhập lại: ");
                }
                else if (orderBL.GetAllOrderByOrderID (order_id).Count == 0) {
                    Console.WriteLine ("Mã đơn hàng không tồn tại !");
                    Console.Write ("Mời bạn nhập lại: ");
                } else {
                    break;
                }
            }
            List<Item> Listitem = new List<Item> ();
            double price = 0;
            Console.Clear ();
            foreach (var order in orderBL.GetAllOrderByOrderID (order_id)) {
                Listitem.Add (itemBL.GetItemByID (order.OrderItem.item_id));
            }
            Console.WriteLine ("|===============================================================================|");
            Console.WriteLine ("- MÃ ĐƠN HÀNG [{0}]", order_id);
            Console.WriteLine ("|===============================================================================|");
            Console.WriteLine ("|{0,-15}|{1,-25}|{2,-25}\t\t|", "Mã sản phẩm", "Tên sản phẩm", "Giá sản phẩm");
            foreach (var item in Listitem) {
                string format = string.Format ($"|{item.item_id,-15}|{item.item_name,-25}|{FormatAndValid.FormatCurrency(item.item_price),-25}\t\t|");
                Console.WriteLine ("|-------------------------------------------------------------------------------|");
                Console.WriteLine (format);
                
                price = price + item.item_price;
            }
            Console.WriteLine ("|-------------------------------------------------------------------------------|");
            Console.WriteLine (" Tổng giá tiền đơn hàng : {0}", FormatAndValid.FormatCurrency (price));
            Console.WriteLine ("|===============================================================================|");
            Console.Write ("Bấm phím bất kì để quay lại");
            Console.ReadKey ();
            GetAllOrdersByUserID(user);
        }
    }
}