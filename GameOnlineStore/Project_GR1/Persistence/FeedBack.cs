using System;

namespace Persistence
{
    public class FeedBack
    {
        
        public FeedBack(){}

        public FeedBack(User user, Item item, int feedback_id, string comment, int rate)
        {
            this.user = user;
            this.item = item;
            Feedback_id = feedback_id;
            Comment = comment;
            Rate = rate;
        }

        public User user {get;set;}
        public Item item { get; set; }
        public int Feedback_id { get; set; }
        public string Comment { get; set; }
        public int Rate { get; set; }

    }
}