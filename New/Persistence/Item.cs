using System;

namespace Persistence
{
    public class Item
    {
        public int ItemId {set;get;}
        public string ItemName {set;get;}
        public decimal ItemPrice {set;get;}
        public string Description {set;get;}

        public override bool Equals(object obj){
            if(obj is Item){
                return ((Item)obj).ItemId.Equals(ItemId);
            }
            return false;
        }

        public override int GetHashCode(){
            return ItemId.GetHashCode();
        }
    }
}