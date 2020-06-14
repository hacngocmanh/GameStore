using System;
using Xunit;
using DAL;
using Persistence;
namespace DAL.Test
{
    public class ItemDALTest
    {
        private ItemDAL itemDAL = new ItemDAL();
        [Fact] // check hiển thị item
        public void TestShowItem()
        {
            Assert.NotNull(itemDAL.GetAllItems());
        }

        // check mã item true
        [Theory] 
        [InlineData(101)]
        [InlineData(102)]
        [InlineData(103)]
        [InlineData(104)]
        [InlineData(105)]
        [InlineData(106)]
        [InlineData(107)]
        [InlineData(108)]
        
        public void GetAnItemByIdTest(int? itemId) 
        {
            Assert.NotNull(itemDAL.GetItemByID(itemId));
        }
        // check mã item false
        [Theory] 
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        
        public void GetAnItemByIdTestFalse(int? itemId) 
        {
            Assert.Null(itemDAL.GetItemByID(itemId));
        }
    }
}
