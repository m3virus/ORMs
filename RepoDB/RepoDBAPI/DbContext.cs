using RepoDb;
using System.Data.SQLite;

namespace RepoDBAPI
{
    public class DbContext : IDisposable
    {
        private readonly string _connectionString;
        public DbContext(IConfiguration configuratioin) => _connectionString = configuratioin.GetConnectionString("SqliteConnection");

        public SQLiteConnection AppDbContext
            => new SQLiteConnection(_connectionString);

        public void Dispose()
        {
            
        }
    }
}
