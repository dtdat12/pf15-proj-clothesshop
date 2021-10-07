using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DAL
{
    public class DBHelper
    {
        private static MySqlConnection connection;
        public static MySqlConnection GetConnection()
        {
            if (connection == null)
            {
                connection = new MySqlConnection
                {
<<<<<<< HEAD
                    ConnectionString = "server=localhost;user id=vtca;password=vtcacademy;port=3306;database=logindb;"
=======
                    ConnectionString = " server=localhost;user id=vtca;password=vtcacademy;port=3306;database=LoginDB; "
>>>>>>> a323dde2b37753a0b80afd4caebd54e46a9c496f
                };
            }
            return connection;
        }
<<<<<<< HEAD

        public static MySqlDataReader ExecQuery(string query)
        {
            MySqlCommand command = new MySqlCommand(query, connection);
            return command.ExecuteReader();
        }

        public static long ExecuteNonQuery(string query)
        {
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return command.LastInsertedId;
        }

        public static MySqlConnection OpenConnection()
        {
            if (connection == null)
            {
                GetConnection();
            }
            connection.Open();
            return connection;
        }

        public static void CloseConnection()
        {
            if (connection != null) connection.Close();
        }
        
        public static bool ExecTransaction(List<string> queries)
        {
            bool result = true;
            OpenConnection();
            MySqlCommand command = connection.CreateCommand();
            MySqlTransaction trans = connection.BeginTransaction();

            command.Connection = connection;
            command.Transaction = trans;

            try
            {
                foreach (var query in queries)
                {
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                    trans.Commit();
                }
                result = true;
            }
            catch
            {
                result = false;
                try
                {
                    trans.Rollback();
                }
                catch
                {
                }
            }
            finally
            {
                CloseConnection();
            }
            return result;
        }
    }
}
=======
        private DbHelper(){}
    }
}
>>>>>>> a323dde2b37753a0b80afd4caebd54e46a9c496f
