using System.Data;
using Inventory.Entities;
using Inventory.Persistence.Interfaces.CRUD;
using Oracle.ManagedDataAccess.Client;

namespace Inventory.Persistence.Repositories.CRUD
{
    public class UserRepository : DBRepository, IUserRepository
    {
        public UserRepository(OracleDB connection) : base(connection)
        {
            
        }

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
                user = new User
                {
                    Id              = Convert.ToInt32(data.Rows[0]["id"]),
                    Role            = new Role
                    {
                        Id          = Convert.ToInt32(data.Rows[0]["role_id"]),
                        Name        = data.Rows[0]["role_name"].ToString(),
                        Description = data.Rows[0]["role_description"].ToString()
                    },
                    Name            = data.Rows[0]["name"].ToString(),
                    Email           = data.Rows[0]["email"].ToString()
                };
            }

            return user;
        }

        public async Task<List<User>> GetAllAsync()
        {            
            // Ejecutamos el procedimiento
            var data = await _connection.ExecuteProcedure(
                spName: "proc_users_get_all",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("v_users", OracleDbType.RefCursor, ParameterDirection.Output)
                }
            );

            // Obtenemos la lista
            List<User> result = new List<User>();

            foreach (DataRow row in data.Rows)
            {
                result.Add(new User
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
                });
            }

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

            // Obtenemos la entidad
            if (data.Rows.Count > 0)
            {
                user = new User
                {
                    Id              = Convert.ToInt32(data.Rows[0]["id"]),
                    Role            = new Role
                    {
                        Id          = Convert.ToInt32(data.Rows[0]["role_id"]),
                        Name        = data.Rows[0]["role_name"].ToString(),
                        Description = data.Rows[0]["role_description"].ToString()
                    },
                    Name            = data.Rows[0]["name"].ToString(),
                    Email           = data.Rows[0]["email"].ToString()
                };
            }

            return user;
        }

        public async Task<bool> ExistsAsync(string email)
        {
            bool exists = false;

            // Ejecutamos el procedimiento
            var data = await _connection.ExecuteProcedure(
                spName: "proc_users_get_by_email",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("p_user_email", OracleDbType.Varchar2, ParameterDirection.Input, email),
                    _connection.AddParameter("v_user", OracleDbType.RefCursor, ParameterDirection.Output)
                }
            );

            // Obtenemos la entidad
            if (data.Rows.Count > 0)
                exists = true;

            return exists;
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            User? user = null;

            // Ejecutamos el procedimiento
            var data = await _connection.ExecuteProcedure(
                spName: "proc_users_login",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("p_user_email", OracleDbType.Varchar2, ParameterDirection.Input, email),
                    _connection.AddParameter("p_user_password", OracleDbType.Varchar2, ParameterDirection.Input, password),
                    _connection.AddParameter("v_user", OracleDbType.RefCursor, ParameterDirection.Output)
                }
            );

            // Obtenemos la entidad
            if (data.Rows.Count > 0)
            {
                user = new User
                {
                    Id              = Convert.ToInt32(data.Rows[0]["id"]),
                    Role            = new Role
                    {
                        Id          = Convert.ToInt32(data.Rows[0]["role_id"]),
                        Name        = data.Rows[0]["role_name"].ToString(),
                        Description = data.Rows[0]["role_description"].ToString()
                    },
                    Name            = data.Rows[0]["name"].ToString(),
                    Email           = data.Rows[0]["email"].ToString()
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
                    _connection.AddParameter("v_result", OracleDbType.Int32, ParameterDirection.Output)
                }
            );

            // Obtenemos el valor
            if (data > 0)
                success = true;

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
            if (data > 0)
                success = true;

            return success;
        }
    }
}