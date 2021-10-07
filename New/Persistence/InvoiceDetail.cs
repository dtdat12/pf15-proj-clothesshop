using System;

namespace Persistence
{
    public class InvoiceDetail
    {
        public long invoicesNO {set;get;}
        public int itemID {set;get;}
        public decimal itemPrice {set;get;}
        public int quantity{set;get;}
        public ItemDetails itemDetail{set;get;}
        public override string ToString()
        {
            return String.Format("|{0,-5}\t|{1,-15}|{2,-7}|{3,-18}|"
            ,itemID,itemDetail.ColorName,itemDetail.SizeName,itemPrice);
        }
    }
}