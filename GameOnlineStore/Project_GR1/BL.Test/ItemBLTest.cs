using System;
using Xunit;
using BL;
using Persistence;
namespace BL.Test
{
    public class ItemBLTest
    {
        private ItemBL itemBL = new ItemBL();
        [Fact] // check hiển thị item
        public void TestShowItem()
        {
            Assert.NotNull(itemBL.GetAllItems());
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
            Assert.NotNull(itemBL.GetItemByID(itemId));
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
            Assert.Null(itemBL.GetItemByID(itemId));
        }
    }
}
