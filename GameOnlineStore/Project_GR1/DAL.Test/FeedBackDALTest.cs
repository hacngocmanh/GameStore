using System;
using Xunit;
using Persistence;
namespace DAL.Test
{
    public class FeedBackDALTest
    {
        FeedBackDAL fbDAL = new FeedBackDAL();
        [Fact] // Check tạo fb
        public void CreateFeedBackTestTrue()
        {
            Item item = new Item(101,"Quy",999000,"Game Hay","Hanh Dong","Rocstar","2/10/2010","Windown");
            User user = new User(1,"Quy","Quy@gmail.com","123456","0969278736",990000);
            FeedBack fb = new FeedBack(user,item,1,"Game hay qua",5);
            Assert.True(fbDAL.CreateFeedback(fb));
        }
        [Fact] // Check tạo fb
        public void CreateFeedBackTestFalse()
        {
            Item item = new Item(-101,"Quy",999000,"Game Hay","Hanh Dong","Rocstar","2/10/2010","Windown");
            User user = new User(-1,"Quy","Quy@gmail.com","123456","0969278736",990000);
            FeedBack fb = new FeedBack(user,item,1,"Game hay qua",5);
            Assert.False(fbDAL.CreateFeedback(fb));
        }
        [Fact]
        public void CheckFeedBackUserTest()
        {
            Item item = new Item(101,"Quy",999000,"Game Hay","Hanh Dong","Rocstar","2/10/2010","Windown");
            User user = new User(1,"Quy","Quy@gmail.com","123456","0969278736",990000);
            Assert.NotNull(fbDAL.GetFeedBackForCheck(user, item.item_id));
        }
        [Fact]
        public void UpdateFeedBackTest(){
            Item item = new Item(101,"Quy",999000,"Game Hay","Hanh Dong","Rocstar","2/10/2010","Windown");
            User user = new User(1,"Quy","Quy@gmail.com","123456","0969278736",990000);
            FeedBack fb = new FeedBack(user,item,1,"Game Hay",5);
             Assert.True(fbDAL.UpdateFeedBack(fb));
        }
    }
}