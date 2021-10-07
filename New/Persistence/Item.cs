using System;

namespace Persistence
{
    public class Item
    {
        public int ItemId {set;get;}
        public string ItemName {set;get;}
        public decimal ItemPrice {set;get;}
        public string ItemDescription {set;get;}

        public override bool Equals(object obj)
        {
            if(obj is Item)
            {
                return ((Item)obj).ItemId.Equals(ItemId);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return ItemId.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("| {0}\t| {1,-25} \t| {2,-65} \t| {3,-5}\t |",ItemId,ItemName,ItemDescription,String.Format("{0:0,0 VND}",ItemPrice));
        }   
    }
}