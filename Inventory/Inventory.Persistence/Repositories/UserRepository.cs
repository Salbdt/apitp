using System.Data;
using Inventory.Entities;
using Inventory.Persistence.Interfaces;
using Oracle.ManagedDataAccess.Client;

namespace Inventory.Persistence.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public async Task<User?> CreateAsync(User entity)
        {
            User? user = null;
            
            // Ejecutamos el procedimiento
            var data = await _connection.ExecuteProcedure(
                spName: "proc_users_create",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("new_role_id", OracleDbType.Int32, ParameterDirection.Input, entity.Role.Id),
                    _connection.AddParameter("new_name", OracleDbType.Varchar2, ParameterDirection.Input, entity.Name),
                    _connection.AddParameter("new_email", OracleDbType.Varchar2, ParameterDirection.Input, entity.Email),
                    _connection.AddParameter("new_password", OracleDbType.Varchar2, ParameterDirection.Input, entity.Password),
                    _connection.AddParameter("v_user", OracleDbType.RefCursor, ParameterDirection.Output)
                }
            );
        
            // Obetenemos el enumerable
            if (data.Rows.Count > 0)
            {
                DataRow firstRow = data.AsEnumerable().First();

                user = new User
                {
                    Id              = Convert.ToInt32(firstRow["id"]),
                    Role            = new Role
                    {
                        Id          = Convert.ToInt32(firstRow["role_id"]),
                        Name        = firstRow["role_name"].ToString(),
                        Description = firstRow["role_description"].ToString()
                    },
                    Name            = firstRow["name"].ToString(),
                    Email           = firstRow["email"].ToString()
                };
            }

            return user;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {            
            IEnumerable<User>? result = null;
            
            // Ejecutamos el procedimiento
            var data = await _connection.ExecuteProcedure(
                spName: "proc_users_get_all",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("v_users", OracleDbType.RefCursor, ParameterDirection.Output)
                }
            );

            // Obtenemos el enumerable
            result = data.AsEnumerable().Select(
                row => new User
                {
                    Id              = Convert.ToInt32(row["id"]),
                    Role            = new Role
                    {
                        Id          = Convert.ToInt32(row["role_id"]),
                        Name        = row["role_name"].ToString(),
                        Description = row["role_description"].ToString()
                    },
                    Name            = row["name"].ToString(),
                    Email           = row["email"].ToString()
                }
            );

            return result;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            User? user = null;

            // Ejecutamos el procedimiento
            var data = await _connection.ExecuteProcedure(
                spName: "proc_users_get_by_id",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("user_id", OracleDbType.Int32, ParameterDirection.Input, id),
                    _connection.AddParameter("v_user", OracleDbType.RefCursor, ParameterDirection.Output)
                }
            );

            // Obtenemos el enumerable
            if (data.Rows.Count > 0)
            {
                DataRow firstRow = data.AsEnumerable().First();

                user = new User
                {
                    Id              = Convert.ToInt32(firstRow["id"]),
                    Role            = new Role
                    {
                        Id          = Convert.ToInt32(firstRow["role_id"]),
                        Name        = firstRow["role_name"].ToString(),
                        Description = firstRow["role_description"].ToString()
                    },
                    Name            = firstRow["name"].ToString(),
                    Email           = firstRow["email"].ToString()
                };
            }

            return user;
        }

        public async Task<bool> UpdateAsync(int id, string email, string password, User entity)
        {
            bool success = false; 

            // Ejecutamos el procedimiento
            var data = await _connection.ExecuteScalarProcedure(
                spName: "proc_users_update",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("user_id", OracleDbType.Int32, ParameterDirection.Input, id),
                    _connection.AddParameter("user_email", OracleDbType.Varchar2, ParameterDirection.Input, email),
                    _connection.AddParameter("user_password", OracleDbType.Varchar2, ParameterDirection.Input, password),
                    _connection.AddParameter("new_role_id", OracleDbType.Int32, ParameterDirection.Input, entity.Role.Id),
                    _connection.AddParameter("new_name", OracleDbType.Varchar2, ParameterDirection.Input, entity.Name),
                    _connection.AddParameter("new_email", OracleDbType.Varchar2, ParameterDirection.Input, entity.Email),
                    _connection.AddParameter("new_password", OracleDbType.Varchar2, ParameterDirection.Input, entity.Password),
                    _connection.AddParameter("v_user", OracleDbType.RefCursor, ParameterDirection.Output)
                }
            );
            
            // Obtenemos el valor
            if (data == 1 || data == 2)
            {
                success = true;
                Console.WriteLine($"Data: {data}");
            }

            return success;
        }

        public async Task<bool> UpdateAsync(int id, User entity)
        { // No implementado
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool success = false;   

            // Ejecutamos el procedimiento
            var data = await _connection.ExecuteScalarProcedure(
                spName: "proc_users_delete",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("user_id", OracleDbType.Int32, ParameterDirection.Input, id),
                    _connection.AddParameter("v_result", OracleDbType.Int32, ParameterDirection.Output)
                }
            );

            // Obtenemos el valor
            if (data == 1)
                success = true;

            return success;
        }
    }
}