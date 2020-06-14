using System;

namespace Persistence
{
    public class Item
    {
        
        public Item() { }

        public Item(int item_id, string item_name, double item_price, string item_description, string item_tag, string item_developer, string item_publisher, string item_platform)
        {
            this.item_id = item_id;
            this.item_name = item_name;
            this.item_price = item_price;
            this.item_description = item_description;
            this.item_tag = item_tag;
            this.item_developer = item_developer;
            this.item_publisher = item_publisher;
            this.item_platform = item_platform;
        }

        public int item_id { get; set; }
        public string item_name { get; set; }
        public double item_price { get; set; }
        public string item_description { get; set; }
        public string item_tag { get; set; }
        public string item_developer { get; set; }
        public string item_publisher { get; set; }
        public string item_platform { get; set; }
    }

}
