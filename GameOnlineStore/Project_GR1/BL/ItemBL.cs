using System;
using Persistence;
using DAL;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace BL
{
    public class ItemBL
    {
        private ItemDAL itemDAL = new ItemDAL();

        public List<Item> GetAllItems()
        {
            return itemDAL.GetAllItems();
        }
        public ItemBL()
        {
            itemDAL = new ItemDAL();
        }
        public Item GetItemByID(int? item_ID)
        {
            if (item_ID == null)
            {
                return null;
            }
            return itemDAL.GetItemByID(item_ID);
        }
    }
}