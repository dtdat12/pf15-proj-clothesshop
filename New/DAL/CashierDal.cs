using System;
using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class CashierDAL
    {
      public int Login(Cashier cashier)
      {
        int login = 0;
        try
        {
          MySqlConnection connection = DBHelper.GetConnection();
          connection.Open();
          MySqlCommand command = connection.CreateCommand();
          command.CommandText = "select * from Cashiers where user_name='"+
          cashier.UserName+"' and user_pass='"+
          Md5Algorithms.CreateMD5(cashier.Password)+"';";
          MySqlDataReader reader = command.ExecuteReader();
          if(reader.Read())
          {
            login = reader.GetInt32("cashier_id");
          }else{
            login = 0;
          }
          reader.Close();
          connection.Close();
          }catch{
          login = -1;
          }
        Console.WriteLine(login);
        return login;
      }
    }
}