using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using BL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Persistence;
using PL_Console;

namespace PL_Console {
    public class OrderConsole {
        private static MenuShop ms = new MenuShop ();
        private static Menu m = new Menu ();
        private ItemBL itemBL = new ItemBL ();
        private OrderBL orderBL = new OrderBL ();
        private static PurchaseConsole p = new PurchaseConsole ();
        
        public void Order (User user) // thêm sản  phẩm vào giỏ hàng
        {

            string chooseOrder;
            Console.Write ("Bạn có muốn thêm sản phẩm vào giỏ hàng không ?(C/K): ");
            while (true) {
                chooseOrder = Console.ReadLine ().ToUpper ();
                if (chooseOrder != "C" && chooseOrder != "K") {
                    Console.WriteLine ("Bạn đã nhập sai, Hãy nhập lại!");
                    Console.Write ("Bạn có muốn thêm sản phẩm vào giỏ hàng không ?(C/K): ");
                    continue;
                } else {
                    break;
                }
            }
            switch (chooseOrder) {
                case "C":
                    AddToCart (user);
                    break;
                case "K":
                    ms.ShowItems (user);
                    break;
            }
        }
        public void AddToCart (User user) {
            // check xem sản phẩm có trong giỏ hàng chưa
            try {
                foreach (var order in orderBL.LibGame (user.user_id)) {
                    if (MenuShop.itemid == order.OrderItem.item_id) {
                        Console.WriteLine ("Bạn đã sở hữu trò chơi này rồi! ");
                        Console.Write ("Ấn phím bất kì để quay trở lại. ");
                        Console.ReadKey ();
                        ms.ShowItems (user);
                    }
                }
            } catch (System.Exception) {
                Console.WriteLine ("Mất kết nối dữ liệu !");
                Console.WriteLine ("Ấn phím bất kì để quay trở lại Trang chủ !");
                Console.ReadKey ();
                m.MainMenu ();
            }

            // thêm sản phẩm vào giỏ hàng 
            if (!File.Exists ("order" + user.user_id + ".json")) {
                Order order = new Order ();
                order.ListItem = new List<Item> ();
                order.order_date = DateTime.Now;
                order.user = user;
                order.ListItem.Add (itemBL.GetItemByID (MenuShop.itemid));
                File.WriteAllText ("order" + user.user_id + ".json", JsonConvert.SerializeObject (order));
                using (StreamWriter file = File.CreateText ("order" + user.user_id + ".json")) {
                    JsonSerializer serializer = new JsonSerializer ();
                    serializer.Serialize (file, order);
                    file.Close ();
                }
                Console.WriteLine ("Thêm vào giỏ hàng thành công !");
                Console.Write ("Bạn có muốn tiếp tục không(C/K)?: ");
                string choose;
                while (true) {
                    choose = Console.ReadLine ().ToUpper ();
                    if (choose != "C" && choose != "K") {
                        Console.WriteLine ("Bạn đã nhập sai, hãy nhập lại!");
                        Console.Write ("Bạn có muốn tiếp tục không(C/K)?: ");
                        continue;
                    } else {
                        break;
                    }
                }
                switch (choose) {
                    case "C":
                        ms.ShowItems (user);
                        break;
                    case "K":
                        ms.menuShop (user);
                        break;
                }
            } else {
                Order order = new Order ();
                CheckItemInCart (user);
                order.order_date = DateTime.Now;
                order.ListItem = new List<Item> ();
                order.user = user;
                var list = JsonConvert.DeserializeObject<Order> (File.ReadAllText ("order" + user.user_id + ".json"));
                list.ListItem.Add (itemBL.GetItemByID (MenuShop.itemid));
                var convertedJson = JsonConvert.SerializeObject (list, Formatting.Indented);
                File.WriteAllText ("order" + user.user_id + ".json", convertedJson);
                Console.WriteLine ("Thêm vào giỏ hàng thành công !");
                Console.Write ("Bạn có muốn tiếp tục không(C/K)?: ");
                string choose;
                while (true) {
                    choose = Console.ReadLine ().ToUpper ();
                    if (choose != "C" && choose != "K") {
                        Console.WriteLine ("Bạn đã nhập sai, hãy nhập lại!");
                        Console.Write ("Bạn có muốn tiếp tục không(C/K)?: ");
                        continue;
                    } else {
                        break;
                    }
                }
                switch (choose) {
                    case "C":
                        ms.ShowItems (user);
                        break;
                    case "K":
                        ms.menuShop (user);
                        break;
                }
            }
        }
        public void CheckItemInCart (User user) {
            try {
                var lists = JsonConvert.DeserializeObject<Order> (File.ReadAllText ("order" + user.user_id + ".json"));
            } catch (System.Exception) {
                Console.WriteLine ("Mất kết nối dữ liệu");
                Console.Write ("Ấn phím bất kì để quay trở lại Danh sách trò chơi. ");
                Console.ReadKey ();
                ms.ShowItems (user);
            }
            var list = JsonConvert.DeserializeObject<Order> (File.ReadAllText ("order" + user.user_id + ".json"));
            foreach (var item in list.ListItem) {
                if (MenuShop.itemid == item.item_id) {
                    Console.WriteLine ("Sản phẩm đã tồn tại trong giỏ hàng! ");
                    Console.Write ("Ấn phím bất kì để quay trở lại Danh sách trò chơi. ");
                    Console.ReadKey ();
                    ms.ShowItems (user);
                }
            }
        }

        public void ShowCarts (User user) // hiển thị giỏ sản phẩm trong giỏ hàng
        {
            Console.Clear ();
            try {
                StreamReader r = new StreamReader ("order" + user.user_id + ".json");
                r.Close ();
            } catch (SystemException) {
                Console.WriteLine ("|========================================================================|");
                Console.WriteLine ("|-----------------------------| GIỎ HÀNG |-------------------------------|");
                Console.WriteLine ("|========================================================================|");
                Console.WriteLine ("|{0,-15}|{1,-30}|{2,-25}", "Mã sản phẩm", "Tên sản phẩm", "Giá sản phẩm");
                Console.WriteLine ("|------------------------------------------------------------------------|\n");
                Console.WriteLine (" Không có sản phẩm trong giỏ hàng !\n");
                Console.WriteLine ("|========================================================================|");
                Console.WriteLine ("Ấn phím kì để quay trở lại");
                Console.ReadKey ();
                ms.menuShop (user);
            }
            using (StreamReader r = new StreamReader ("order" + user.user_id + ".json")) {
                double price = 0;
                var json = r.ReadToEnd ();
                var ListOrder = JsonConvert.DeserializeObject<Order> (json);
                Console.WriteLine ("|========================================================================|");
                Console.WriteLine ("|-----------------------------| GIỎ HÀNG |-------------------------------|");
                Console.WriteLine ("|========================================================================|");
                Console.WriteLine ("|{0,-15}|{1,-30}|{2,-25}|", "Mã sản phẩm", "Tên sản phẩm", "Giá sản phẩm");
                Console.WriteLine ("|------------------------------------------------------------------------|");
                // kiểm tra nếu item có trong listOrder thì hiển thị
                foreach (var item in ListOrder.ListItem) {
                    string format = string.Format ($"|{item.item_id,-15}|{item.item_name,-30}|{FormatAndValid.FormatCurrency(item.item_price),-25}|");
                    Console.WriteLine (format);
                    Console.WriteLine ("|------------------------------------------------------------------------|");
                    price += item.item_price;
                }
                Console.WriteLine ("\t\t\tTổng cộng : " + FormatAndValid.FormatCurrency (price));
                Console.WriteLine ("|========================================================================|");
                r.Close ();
            }
            int choose;
            Console.WriteLine ("\n1. Thanh toán     ");
            Console.WriteLine ("2. Hủy đơn hàng     ");
            Console.WriteLine ("3. Quay về trang chủ");
            Console.WriteLine ("--------------------");
            Console.Write ("Chọn: ");
            while (true) {
                bool isINT = Int32.TryParse (Console.ReadLine (), out choose);
                if (!isINT) {
                    Console.Write ("Bạn đã nhập sai, hãy nhập lại: ");
                } else if (choose < 1 && choose > 3) {
                    Console.Write ("Bạn đã nhập sai, hãy nhập lại: ");
                } else {
                    break;
                }
            }
            switch (choose) {
                case 1:
                    p.Purchase (user);
                    break;
                case 2:
                    DeleteCart (user);
                    break;
                case 3:
                    ms.menuShop (user);
                    break;
            }
        }
        public void DeleteCart (User user) // hủy đơn hàng 
        {
            try {
                File.Delete ("order" + user.user_id + ".json"); // xóa file json
            } catch (System.Exception) {
                Console.WriteLine ("Hủy đơn hàng không thành công !");
                Console.WriteLine ("Ấn phím bất kì để quay trở lại Trang chủ !");
                Console.ReadKey ();
                m.MainMenu ();
            }
            Console.WriteLine ("Hủy đơn hàng thành công !");
            Console.WriteLine ("Ấn phím bất kì để quay trở lại Trang chủ !");
            Console.ReadKey ();
            ms.menuShop (user);
        }

    }
}