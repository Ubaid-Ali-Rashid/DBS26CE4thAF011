using MySql.Data.MySqlClient;

namespace HotelManagementSystem.Data
{
    public class DbConnection
    {
        private readonly string _connectionString;

        public DbConnection()
        {
            _connectionString = "Server=localhost;Database=HotelBookingSystem;Uid=root;Pwd=;";
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}