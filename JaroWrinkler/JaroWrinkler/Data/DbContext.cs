using Oracle.ManagedDataAccess.Client;

namespace JaroWrinkler.Data
{
    public class DbContext
    {
        private readonly string _connectionString;
        public DbContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        public OracleConnection CreateConnection()
        {
            return new OracleConnection(_connectionString);
        }
    }
}