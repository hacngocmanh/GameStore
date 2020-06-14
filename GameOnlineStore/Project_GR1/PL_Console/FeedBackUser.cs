using System;
using System.Text;
using BL;
using Persistence;
namespace PL_Console {
    public class FeedBackConsole {
        private UserBL userBL = new UserBL ();
        private FeedBackBL FeedbackBL = new FeedBackBL ();
        private ItemBL itemBL = new ItemBL ();
        private FeedBack FB = new FeedBack ();
        private static MenuShop ms = new MenuShop ();
        private static Menu m = new Menu ();

        // Tạo phản hồi từ khách hàng
        public void CreateFeedBack (User user) {

            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            CheckFeedBackUser (user);
            Console.Clear ();
            Console.WriteLine ("|===================================================|");
            Console.WriteLine ("|---------| CẢM NHẬN VÀ ĐÁNH GIÁ TRÒ CHƠI |---------|");
            Console.WriteLine ("|===================================================|");

            Console.Write (" \nCảm nhận của bạn !\n- ");
            string Comment;
            while (true) {
                Comment = Console.ReadLine ();
                if (Comment.Length <= 0) {
                    Console.Write ("Bạn phải ghi ít nhất 1 kí tự !\n- ");

                } else {
                    break;
                }
            }

            Console.WriteLine ("Đánh giá của bạn từ 1 đến 5 !");
            Console.Write ("Số điểm đánh giá của bạn: ");

            int Rate;
            while (true) {
                bool isInt = Int32.TryParse (Console.ReadLine (), out Rate);
                if (!isInt) {
                    Console.WriteLine ("Bạn đã nhập sai! hãy nhập lại!");
                    Console.Write ("Số điểm đánh giá của bạn: ");
                } else if (Rate < 1 || Rate > 5) {
                    Console.WriteLine ("Giới hạn từ 1 đến 5 điểm ");
                    Console.Write ("Số điểm đánh giá của bạn: ");
                } else {
                    break;
                }
            }
            // Gán comment rate user_id và item_id
            FB.Comment = Comment;
            FB.Rate = Rate;
            FB.user = user;
            FB.item = itemBL.GetItemByID (MenuShop.itemid);

            if (FeedbackBL.CreateFeedBack (FB) == true) {
                Console.WriteLine ("\nPhản hồi của bạn đã đăng tải thành công !");
                Console.WriteLine ("Bấm phím bất kì để tiếp tục ");
                Console.ReadKey ();
                ms.ShowItems (user);
            } else {
                Console.WriteLine ("Mất kết nối dữ liệu !");
                Console.WriteLine ("Bấm phím bất kì để quay về trang chủ");
                Console.ReadKey ();
                m.MainMenu ();
            }
        }
        // kiểm tra fb xem có trong sản phẩm hay chưa 
        public void CheckFeedBackUser (User user) {
            
            // kiểm tra fb trong shop
            if (FeedbackBL.GetFeedBackForCheck (user, MenuShop.itemid).Feedback_id != 0) {
                Console.Clear ();
                Console.WriteLine ("|===================================================|");
                Console.WriteLine ("|---------| CẢM NHẬN VÀ ĐÁNH GIÁ TRÒ CHƠI |---------|");
                Console.WriteLine ("|===================================================|\n");

                Console.WriteLine ("Bài đánh giá của bạn đã có trong sản phẩm này rồi!");
                Console.Write ("Bạn có muốn chỉnh sửa lại không?(C/K): ");

                string choose;
                while (true) {
                    choose = Console.ReadLine ().ToUpper ();
                    if (choose != "C" && choose != "K") {
                        Console.WriteLine ("Bạn đã nhập sai, hãy nhập lại!");
                        Console.Write ("Bạn có muốn chỉnh sửa lại không?(C/K): ");
                        continue;
                    } else {
                        break;
                    }
                }
                switch (choose) {
                    case "C":
                        UpdateFeedBack (user);
                        break;
                    case "K":
                        ms.ShowItems (user);
                        break;
                }
            }
        }
        // Chỉnh sửa phản hồi
        public void UpdateFeedBack (User user) {

            Console.Write ("Cảm nhận của bạn !\n- ");
            string Comment = Console.ReadLine ();
            Console.WriteLine ("Đánh giá của bạn từ 1 đến 5 ! ");
            Console.Write ("Số điểm đánh giá của bạn: ");
            int Rate;
            while (true) {
                bool isInt = Int32.TryParse (Console.ReadLine (), out Rate);
                if (!isInt) {
                    Console.WriteLine ("Bạn đã nhập sai, hãy nhập lại!");
                    Console.WriteLine ("Số điểm đánh giá của bạn: ");
                } else if (Rate < 1 || Rate > 5) {
                    Console.WriteLine ("Giới hạn từ 1 đến 5 điểm !");
                    Console.Write ("Số điểm đánh giá của bạn: ");
                } else {
                    break;
                }
            }
            // gán lại comment rate user_id và item_id
            FB.Comment = Comment;
            FB.Rate = Rate;
            FB.user = user;
            FB.item = itemBL.GetItemByID (MenuShop.itemid);

            if (FeedbackBL.UpdateFeedBack (FB) == true) {
                Console.WriteLine ("Cập nhật bài đánh giá thành công !");
                Console.WriteLine ("Bấm phím bất kì để tiếp tục !");
                Console.ReadKey ();
                ms.ShowItems (user);
            } else {
                Console.WriteLine ("Cập nhật bài đánh giá không thành công !");
                Console.WriteLine ("Bấm phím bất kì để quay về trang chủ");
                Console.ReadKey ();
                m.MainMenu ();
            }
        }
        // hiển thị phản hồi
        public void ShowFeedBacks (User user) {
            int rate = 0;
            int rateAVG = 0;
            int count = FeedbackBL.GetFeedBackByItemId (MenuShop.itemid).Count;

            foreach (var item in FeedbackBL.GetFeedBackByItemId (MenuShop.itemid)) {
                rate = rate + item.Rate;
            }
            if (count == 0) {
                Console.Write ("\n☆ ☆ ☆ ☆ ☆ ({0} Xếp hạng đánh giá)\n", count);
            } else {
                Console.WriteLine ();
                rateAVG = rate / FeedbackBL.GetFeedBackByItemId (MenuShop.itemid).Count;
                RateAVG (rateAVG, count);
            }
            Console.WriteLine ("\n-----------------------");
            Console.WriteLine ("| ĐÁNH GIÁ TRÒ CHƠI   |");
            Console.WriteLine ("-----------------------");
            
            // hiển thị tên cmt và rate
            foreach (var item in FeedbackBL.GetFeedBackByItemId (MenuShop.itemid)) {
                Console.WriteLine ("\nKhách hàng : " + userBL.GetUserInfo (item.user.user_id).user_name);
                Console.WriteLine ("Phản hồi   : " + item.Comment);
                Console.Write ("Rate       : ");
                DrawStars (item.Rate);
            }
        }
        // hiển thị  1 -> 5 sao
        public void DrawStars (int rate) {
            switch (rate) {
                case 1:
                    Console.WriteLine ("★ ☆ ☆ ☆ ☆");
                    break;
                case 2:
                    Console.WriteLine ("★ ★ ☆ ☆ ☆");
                    break;
                case 3:
                    Console.WriteLine ("★ ★ ★ ☆ ☆");
                    break;
                case 4:
                    Console.WriteLine ("★ ★ ★ ★ ☆");
                    break;
                case 5:
                    Console.WriteLine ("★ ★ ★ ★ ★");
                    break;
            }
        }
        public void RateAVG (int rate, int count) {
            switch (rate) {
                case 1:
                    Console.Write ("★ ☆ ☆ ☆ ☆ ({0} Xếp hạng đánh giá)\n", count);
                    break;
                case 2:
                    Console.Write ("★ ★ ☆ ☆ ☆ ({0} Xếp hạng đánh giá)\n", count);
                    break;
                case 3:
                    Console.Write ("★ ★ ★ ☆ ☆ ({0} Xếp hạng đánh giá)\n", count);
                    break;
                case 4:
                    Console.Write ("★ ★ ★ ★ ☆ ({0} Xếp hạng đánh giá)\n", count);
                    break;
                case 5:
                    Console.Write ("★ ★ ★ ★ ★ ({0} Xếp hạng đánh giá)\n", count);
                    break;
            }
        }
    }
}