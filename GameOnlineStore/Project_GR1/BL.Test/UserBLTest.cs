using System;
using Xunit;
using BL;
using Persistence;
using System.IO;
using System.Collections.Generic;

namespace BL.Test {
    public class UserBLTest {
        private static UserBL userbl = new UserBL ();
        [Fact]
        public void UserBLTestTrue () {
            Assert.NotNull (userbl.GetUser ("tk01", "123456"));
        }
        [Fact]
        public void UserBLTestFalse () {
            Assert.Null (userbl.GetUser ("abcxyz", "-1"));
        }
        [Theory]
        [InlineData ("abc", "00000000")]
        [InlineData ("xyz", "00000000")]
         public void TestDataLoginOnLineGameStore (string userName, string PassWord) {
            Assert.Null (userbl.GetUser (userName, PassWord));
        }
    }
}
