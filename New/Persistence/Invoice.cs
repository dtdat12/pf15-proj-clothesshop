using System;

namespace Persistence
{
    public class Invoice
    {
        public long invoicesNO {set;get;}
        public long CashierID {set;get;}
        public long CustomerID {set;get;}
        public DateTime invoiceDate {set;get;}  
        public override string ToString()
        {
            return String.Format("Invoice No: {0,-43}Invoice Date: {1}",invoicesNO,invoiceDate);
        }
    }
}