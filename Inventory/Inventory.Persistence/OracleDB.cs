using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace Inventory.Persistence
{
    public class OracleDB
    {
        public OracleConnection _connection { get; }

        public OracleDB(string connectionString)
        {
            _connection = new OracleConnection(connectionString);
        }

        private OracleCommand CreateCommand(string text, CommandType commandType)
        {
            return new OracleCommand
            {
                Connection = _connection,
                CommandText = text,
                CommandType = commandType
            };
        }

        public async Task<DataTable> ExecuteProcedure(string spName, List<OracleParameter> parameters)
        {
            DataTable data = new DataTable();

            OracleCommand command = CreateCommand(spName, CommandType.StoredProcedure);

            foreach (OracleParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }

            try
            {                
                _connection.Open();
                data.Load(await command.ExecuteReaderAsync());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }

            return data;
        }

        public async Task<long> RunScalarQuery(string query)
        {
            long scalar = 0;

            OracleCommand command = CreateCommand(query, CommandType.Text);

            try
            {                
                _connection.Open();
                scalar = Convert.ToInt64(await command.ExecuteScalarAsync());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }

            return scalar;
        }

        public async Task<DataTable> RunQuery(string query)
        {
            DataTable data = new DataTable();

            OracleCommand command = CreateCommand(query, CommandType.Text);

            try
            {                
                _connection.Open();
                data.Load(await command.ExecuteReaderAsync());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }

            return data;
        }

        public OracleParameter AddParameter(string name, OracleDbType type, ParameterDirection direction, object value)
        {
            return new OracleParameter
            {
                ParameterName   = name,
                OracleDbType    = type,
                Direction       = direction,
                Value           = value
            };
        }

        public OracleParameter AddParameter(string name, OracleDbType type, ParameterDirection direction)
        {
            return new OracleParameter
            {
                ParameterName   = name,
                OracleDbType    = type,
                Direction       = direction
            };
        }
    }
}