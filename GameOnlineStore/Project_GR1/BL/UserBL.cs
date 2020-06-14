using System;
using System.Text.RegularExpressions;
using DAL;
using Persistence;
namespace BL
{
    public class UserBL
    {
        private UserDAL userDAL = new UserDAL();
        public UserBL()
        {
            userDAL = new UserDAL();
        }
        public User GetUser(string username, string password)
        {
            return userDAL.GetUser(username, password);
        }
        public User GetUserInfo(int userid)
        {
            return userDAL.GetUserByID(userid);
        }
        
        public bool Addfund(User user, double values)
        {
            return userDAL.AddFund(user,values);
        }
        
    }
}
