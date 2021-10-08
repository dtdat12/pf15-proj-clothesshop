using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class CustomerDAL
    {
        private string query;
        private MySqlDataReader reader;
        
        public Customer GetCustomerByID(int cusID)
        {
            query = @"select * from customers where customer_id = "+cusID;
            DBHelper.OpenConnection();
            reader = DBHelper.ExecQuery(query);
            Customer cus = null;
            if (reader.Read())
            {
                cus = GetCustomer(reader);
            }
            DBHelper.CloseConnection();
            return cus;
        }

        public Customer GetCustomer(MySqlDataReader reader)
        {
            Customer customer = new Customer();
            customer.CustomerID = reader.GetInt32("customer_id");
            customer.CustomerName = reader.GetString("customer_name");
            customer.CustomerAddress = reader.GetString("customer_address");
            customer.Telephone = reader.GetString("telephone");
            return customer;
        }
        
        public long InsertCustomer(Customer customer)
        {
            query = String.Format(@"insert into customers values(null,'{0}','{1}','{2}');SELECT LAST_INSERT_ID() as lastID;",customer.CustomerName,customer.CustomerAddress,customer.Telephone);
            DBHelper.OpenConnection();
            long lastID = DBHelper.ExecuteNonQuery(query);
            DBHelper.CloseConnection();
            return lastID;
        }
    }
}