using System;

namespace Persistence
{
    public class Customer
    {
        public int CustomerID {set;get;}
        public string CustomerName {set;get;}
        public string CustomerAddress {set;get;}
        public string Telephone {set;get;}

        public override string ToString()
        {
            return String.Format("Customer ID: {0,-40}  Customer Name: {1,-40}\nCus Address: {2,-40}  Cus Telephone: {3,-40}"
            ,CustomerID,CustomerName,CustomerAddress,Telephone);
        }
    }
}