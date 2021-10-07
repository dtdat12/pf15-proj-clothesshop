using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class InvoiceDAL
    {
        public long InsertInvoice(Invoice invoice)
        {
            string query;
            query = String.Format(@"insert into invoices values({0},{1},{2},'{3}');",invoice.invoicesNO,invoice.CashierID,invoice.CustomerID,invoice.invoiceDate.ToString("yyyy-MM-dd HH:mm:ss"));
            DBHelper.OpenConnection();
            long lastID = DBHelper.ExecuteNonQuery(query);
            DBHelper.CloseConnection();
            return lastID;
        }
    }
}