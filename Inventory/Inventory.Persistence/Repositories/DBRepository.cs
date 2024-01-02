using System.Data;

namespace Inventory.Persistence.Repositories
{
    public class DBRepository
    {
        protected readonly OracleDB _connection;

        public DBRepository(OracleDB connection)
        {
            // _connection = new OracleDB("Data Source=//localhost:1521/xepdb1;User Id=inventory;Password=1011SQLexp");
            _connection = connection;
        }
    }
}