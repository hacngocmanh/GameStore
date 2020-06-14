using System;
using System.Collections.Generic;
using DAL;
using Persistence;

namespace BL
{
    public class FeedBackBL
    {
        private FeedBackDAL FbDAL = new FeedBackDAL();
        public bool CreateFeedBack(FeedBack Feedback)
        {
            return FbDAL.CreateFeedback(Feedback);
        }
        public FeedBack GetFeedBackForCheck(User user, int itemid)
        {
            return FbDAL.GetFeedBackForCheck(user,itemid);
            
        }
        public List<FeedBack> GetFeedBackByItemId(int itemid)
        {
            return FbDAL.GetFeedBackByItemID(itemid);
        }
        public bool UpdateFeedBack(FeedBack FeedBack)
        {
            return FbDAL.UpdateFeedBack(FeedBack);
        }
    }
}