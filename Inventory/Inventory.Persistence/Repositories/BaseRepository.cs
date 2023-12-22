namespace Inventory.Persistence.Repositories
{
    public class BaseRepository
    {
        // TODO Cambiar tipo de dato por el de la DB
        protected readonly OracleDB _connection;

        public BaseRepository()
        {
            _connection = new OracleDB();
        }
    }
}