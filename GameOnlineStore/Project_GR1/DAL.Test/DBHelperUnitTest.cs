using System;
using Xunit;
using Persistence;
using System.Collections.Generic;

namespace DAL.Test {
    public class DBHelperUnitTest {

        [Fact] // Check Connection
        public void OpenConnectionTest () {
            Assert.NotNull (DBHelper.OpenConnection ());
        }

        private static string connectionString = $"server=localhost;user id=root;password=Anhquy@123456;port=3306;database=OnlineGameStore;SslMode=none";
        [Fact] // Check Connection true
        public void OpenConnectionWithStringTrueTest () {
            Assert.NotNull (DBHelper.OpenConnection (connectionString));
        }
        private static string connectionString1 = $"server=localhost1;user id=root;password=abcxyz;port=3306;database=OnlineGameStore;SslMode=none";
        [Fact] // Check Connection false
        public void OpenConnectionWithStringFailTest () {
            Assert.Null (DBHelper.OpenConnection(connectionString1));
        }

    }
}