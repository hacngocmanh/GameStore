using System;

namespace Persistence
{
    public class User
    {
        public User() { }
        public User(int user_id, string user_name, string user_email, string password, string phone, double user_balance)
        {
            this.user_id = user_id;
            this.user_name = user_name;
            this.user_email = user_email;
            this.password = password;
            this.phone = phone;
            this.user_balance = user_balance;
        }

        public int user_id { get; set; }
        public string user_name { get; set; }
        public string user_email { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public double user_balance { get; set; }

    }
}
