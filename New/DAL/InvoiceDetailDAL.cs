using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class invoiceDetailDAL
    {
        private MySqlDataReader reader;
        public bool InsertInvoiceDetail(InvoiceDetail invoiceDetail)
        {
            string query;
            query = String.Format(@"insert into invoicedetails values({0},{1},{2},{3},{4},{5})"
            ,invoiceDetail.invoicesNO,invoiceDetail.itemID,invoiceDetail.itemPrice,invoiceDetail.quantity,invoiceDetail.itemDetail.ColorID,invoiceDetail.itemDetail.SizeID);
            DBHelper.OpenConnection();
            reader = DBHelper.ExecQuery(query);
            DBHelper.CloseConnection();
            return true;
        }
    }
}