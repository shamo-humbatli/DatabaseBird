/**
 * <summary>
 * author: Shamo Humbatli
 * </summary>
 */

namespace DatabaseBird
{
    public class DatabaseConfiguration
    {
        private string server;
        private int port;
        private string database;
        private string username;
        private string password;

        public DatabaseConfiguration()
        {

        }

        public DatabaseConfiguration(string server, int port, string database, string username, string password)
        {
            this.Server = server;
            this.Port = port;
            this.Database = database;
            this.Username = username;
            this.Password = password;
        }

        public string Server { get => server; set => server = value; }
        public int Port { get => port; set => port = value; }
        public string Database { get => database; set => database = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
    }
}