using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public static class ItemFilter
    {
        public const int GET_ALL = 0;
        public const int FILTER_BY_ITEM_NAME = 1;
    }
    
    public class ItemDAL
    {
        private string query;
        private MySqlDataReader reader;
        public Item GetItemById(int itemId)
        {
            query = @"select item_id, item_name, item_price, 
                        ifnull(item_description, '') as item_description
                        from Items where item_id=" + itemId + ";";
            DBHelper.OpenConnection();
            reader = DBHelper.ExecQuery(query);
            Item item = null;
            if (reader.Read())
            {
                item = GetItem(reader);
            }
            DBHelper.CloseConnection();
            return item;
        }

        public List<ItemDetails> GetItemDetailByItemID(int itemID)
        {
            List<ItemDetails> lstItem = new List<ItemDetails>();
            query = @"select itemdetails.* , colors.color_name as color_name , 
                       sizes.size_name AS size_name
                       FROM itemdetails,colors,sizes WHERE itemdetails.color_id = colors.color_id and itemdetails.size_id = sizes.size_id and itemdetails.item_id = "+itemID;
            DBHelper.OpenConnection();
            reader = DBHelper.ExecQuery(query);
            while (reader.Read())
            {
                lstItem.Add(GetItemDetail(reader));
            }
            reader.Close();
            DBHelper.CloseConnection();
            return lstItem;
        }

        public List<Item> GetByName(String name)
        {
            List<Item> lstItem = new List<Item>();
             query = @"select item_id, item_name, item_price, 
                        ifnull(item_description, '') as item_description
                        from Items where item_name like '%" + name + "%' ;";
            DBHelper.OpenConnection();
            reader = DBHelper.ExecQuery(query);
            while (reader.Read())
            {
                lstItem.Add(GetItem(reader));
            }
            reader.Close();
            DBHelper.CloseConnection();
            return lstItem;
        }

        public List<Item> GetByPriceRange(int f, int t)
        {
            List<Item> lstItem = new List<Item>();
             query = @"select item_id, item_name, item_price, 
                        ifnull(item_description, '') as item_description
                        from Items where item_price >= "+f+" and item_price <= "+t+" ;";
            DBHelper.OpenConnection();
            reader = DBHelper.ExecQuery(query);
            while (reader.Read())
            {
                lstItem.Add(GetItem(reader));
            }
            reader.Close();
            DBHelper.CloseConnection();
            return lstItem;
        }

        private Item GetItem(MySqlDataReader reader)
        {
            Item item = new Item();
            item.ItemId = reader.GetInt32("item_id");
            item.ItemName = reader.GetString("item_name");
            item.ItemPrice = reader.GetDecimal("item_price");
            item.ItemDescription = reader.GetString("item_description");
            return item;
        }

        private ItemDetails GetItemDetail(MySqlDataReader reader)
        {
            ItemDetails itemDetails = new ItemDetails();
            itemDetails.ItemId = reader.GetInt32("item_id");
            itemDetails.ColorName = reader.GetString("color_name");
            itemDetails.SizeName = reader.GetString("size_name");
            itemDetails.Quantity = reader.GetInt32("quantity");
            itemDetails.ColorID = reader.GetInt32("color_id");
            itemDetails.ColorID = reader.GetInt32("size_id");
            return itemDetails;
        }
        
        private List<Item> GetItems(MySqlCommand command)
        {
            List<Item> lstItem = new List<Item>();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                lstItem.Add(GetItem(reader));
            }
            reader.Close();
            DBHelper.CloseConnection();
            return lstItem;
        }

        public List<Item> GetItems(int itemFilter, Item item)
        {
            MySqlCommand command = new MySqlCommand("", DBHelper.OpenConnection());
            switch(itemFilter)
            {
                case ItemFilter.GET_ALL:
                    query = @"select item_id, item_name, item_price, ifnull(item_description, '') as item_description from Items";
                    break;
                case ItemFilter.FILTER_BY_ITEM_NAME:
                    query = @"select item_id, item_name, item_price, ifnull(item_description, '') as item_description from Items
                                where item_name like concat('%',@itemName,'%');";
                    command.Parameters.AddWithValue("@itemName", item.ItemName);
                    break;
            }
            command.CommandText = query;
            return GetItems(command);
        }
    }
}