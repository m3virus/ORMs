using Microsoft.Data.Sqlite;

namespace RepoDBAPI
{
    public class DbContext : IDisposable
    {
        private readonly string _connectionString;
        public DbContext(IConfiguration configuratioin)
        {
            _connectionString = configuratioin.GetConnectionString("SqliteConnection");
        }

        public SqliteConnection AppDbContext
            => new SqliteConnection(_connectionString);

        public void Dispose()
        {
            
        }
    }
}
