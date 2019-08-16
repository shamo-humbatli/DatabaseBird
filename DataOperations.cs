using MySql.Data.MySqlClient;
using System;
using System.Data;

/**
 * <summary>
 * author: Shamo Humbatli
 * </summary>
 */

namespace DatabaseBird
{
    public class DataOperations
    {

        private MySqlConnection dbSqlConnection;
        private MySqlCommand mySqlCommand;

        public DataOperations()
        {

        }

        public MySqlConnection DbSqlConnection { get => dbSqlConnection; set => dbSqlConnection = value; }

        public void BuildConnection(DatabaseConfiguration databaseConfiguration)
        {
            DbSqlConnection = new MySqlConnection(String.Format("Server = {0}; Port = {1}; Database = {2}; Uid = {3}; Pwd = {4}; CHARSET=UTF8;", databaseConfiguration.Server, databaseConfiguration.Port, databaseConfiguration.Database, databaseConfiguration.Username, databaseConfiguration.Password));
        }

        public DataTable GetData(string tableName, string query, int pageSize = 20, int page = 1)
        {
            try
            {
                DataTable dataTable = new DataTable(tableName);

                int startingRow = page * pageSize - pageSize;
                string myQuery = String.Concat(new string[]
                {
                query,
                " LIMIT " + startingRow + ", " + pageSize
                });

                mySqlCommand = new MySqlCommand(myQuery, DbSqlConnection);
                mySqlCommand.CommandType = CommandType.Text;
                mySqlCommand.Connection = DbSqlConnection;

                if (dbSqlConnection.State != ConnectionState.Open)
                {
                    DbSqlConnection.Open();
                }

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand))
                {
                    adapter.Fill(dataTable);
                }

                return dataTable;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (DbSqlConnection.State == ConnectionState.Open)
                {
                    DbSqlConnection.Close();
                }
            }
        }

        public DataTable GetData(string tableName, MySqlCommand command, int pageSize = 20, int page = 1)
        {

            try
            {
                DataTable dataTable = new DataTable(tableName);

                int startingRow = page * pageSize - pageSize;

                command.CommandText += " LIMIT " + startingRow + ", " + pageSize;
                command.CommandType = CommandType.Text;
                command.Connection = DbSqlConnection;

                if (dbSqlConnection.State != ConnectionState.Open)
                {
                    DbSqlConnection.Open();
                }


                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }

                return dataTable;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (DbSqlConnection.State == ConnectionState.Open)
                {
                    DbSqlConnection.Close();
                }
            }
        }

        public DataTable GetData(string tableName, string query)
        {
            try
            {
                DataTable dataTable = new DataTable(tableName);

                mySqlCommand = new MySqlCommand(query, DbSqlConnection);
                mySqlCommand.CommandType = CommandType.Text;
                mySqlCommand.Connection = DbSqlConnection;

                if (dbSqlConnection.State != ConnectionState.Open)
                {
                    DbSqlConnection.Open();
                }


                using (MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand))
                {
                    adapter.Fill(dataTable);
                }

                return dataTable;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (DbSqlConnection.State == ConnectionState.Open)
                {
                    DbSqlConnection.Close();
                }
            }
        }

        public int SaveData(string query)
        {
           // MySqlTransaction t = mySqlConnection.BeginTransaction();
            try
            {
                if (dbSqlConnection.State != ConnectionState.Open)
                {
                    DbSqlConnection.Open();
                }

                mySqlCommand = new MySqlCommand(query, DbSqlConnection);
                mySqlCommand.CommandType = CommandType.Text;
                mySqlCommand.Connection = DbSqlConnection;

                int r = mySqlCommand.ExecuteNonQuery();
               // t.Commit();

                return r;
            }
            catch (Exception exp)
            {
              //  t.Rollback();
              //  t.Dispose();
                return 0;
            }
            finally
            {
                if (DbSqlConnection.State == ConnectionState.Open)
                {
                    DbSqlConnection.Close();
                }
            }
        }

        public void CloseConnection()
        {
            if(dbSqlConnection.State == ConnectionState.Open)
            {
                dbSqlConnection.Close();
            }
        }
    }


}