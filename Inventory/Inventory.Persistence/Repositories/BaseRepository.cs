using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace Inventory.Persistence.Repositories
{
    public class BaseRepository
    {
        protected readonly OracleDB _connection;

        public BaseRepository()
        {
            // TODO Cambiar modo de inicializar OracleDB
            _connection = new OracleDB("Data Source=//localhost:1521/xepdb1;User Id=inventory;Password=1011SQLexp");
        }
    }
}