using System;
using Persistence;
using Xunit;
namespace DAL.Test {
    public class UserDALTest {
        private UserDAL userDAL = new UserDAL ();
        // check user và password true
        [Fact]
        public void TestLogin1True () {
            Assert.NotNull (userDAL.GetUser ("tk01", "123456"));
        }
        
        // check user và password false
        [Fact]
        public void TestLogin1False () {
            Assert.Null (userDAL.GetUser ("abc", "321"));
        }
        [Fact]
        public void AddFundTestTrue () {
            User user = new User (1, "Quy", "Quy@gmail.com", "123456", "0969278736", 990000);
            Assert.True (userDAL.AddFund (user, 888888));
        }
        [Fact]
        public void AddFundTestFalse () {
            User user = new User (1, "Quy", "Quy@gmail.com", "123456", "0969278736", 990000);
            Assert.False (userDAL.AddFund (user, -99));
        }
        [Fact]
        public void GetUserTestTrue () {
            Assert.NotNull (userDAL.GetUserByID (1));
        }

        [Fact]
        public void GetUserTestFalse () {
            Assert.Null (userDAL.GetUserByID (999));
        }

    }
}