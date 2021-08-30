using System;
using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class CashierDal{
      public int Login(Cashier cashier){
        int login = 0;
        try{
          MySqlConnection connection = DbHelper.GetConnection();
          connection.Open();
          MySqlCommand command = connection.CreateCommand();
          command.CommandText = "select * from Cashiers where username='"+
          cashier.UserName+"' and password='"+
          Md5Algorithms.CreateMD5(cashier.Password)+"';";
          MySqlDataReader reader = command.ExecuteReader();
          reader.Close();
          connection.Close();
        }catch{
          login = -1;
        }
        return login;
      }
    }
}
