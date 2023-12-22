using System.Data;
using Inventory.Entities;
using Inventory.Persistence.Interfaces;
using Oracle.ManagedDataAccess.Client;

namespace Inventory.Persistence.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository()
        {            
        }

        public async Task<User> CreateAsync(User entity)
        {
            User user = new User();
            DataRow firstRow;
            
            // Ejecutamos el procedimiento en la BD
            var data = await _connection.ExecuteProcedure(
                spName: "proc_users_create",
                parameters: new List<OracleParameter>()
                {
                    new OracleParameter
                    {
                        ParameterName = "new_role_id",
                        OracleDbType = OracleDbType.Int32,
                        Direction = ParameterDirection.Input,
                        Value = entity.Role.Id
                    },
                    new OracleParameter
                    {
                        ParameterName = "new_name",
                        OracleDbType = OracleDbType.Varchar2,
                        Direction = ParameterDirection.Input,
                        Value = entity.Name
                    },
                    new OracleParameter
                    {
                        ParameterName = "new_email",
                        OracleDbType = OracleDbType.Varchar2,
                        Direction = ParameterDirection.Input,
                        Value = entity.Email
                    },
                    new OracleParameter
                    {
                        ParameterName = "new_password",
                        OracleDbType = OracleDbType.Varchar2,
                        Direction = ParameterDirection.Input,
                        Value = entity.Password
                    },
                    new OracleParameter
                    {
                        ParameterName = "v_user",
                        OracleDbType = OracleDbType.RefCursor,
                        Direction = ParameterDirection.Output
                    }
                }
            );
            
            // Transformamos los datos recibidos en un Enumerable
            if (data.Rows.Count > 0)
            {
                firstRow = data.AsEnumerable().First();

                user.Id = Convert.ToInt32(firstRow["id"]);
                user.Role = new Role
                {
                    Id = Convert.ToInt32(firstRow["role_id"]),
                    Name = firstRow["role_name"].ToString()
                };
                user.Name = firstRow["name"].ToString();
                user.Email = firstRow["email"].ToString();
            }

            return user;
        }

        //public async Task<IEnumerable<Rol>> GetAllAsync()
        public async Task<IEnumerable<User>> GetAllAsync()
        {            
            IEnumerable<User>? result = null;
            
            // Ejecutamos el procedimiento en la BD
            var data = await _connection.ExecuteProcedure(
                spName: "proc_users_get_all",
                parameters: new List<OracleParameter>()
                {
                    new OracleParameter
                    {
                        ParameterName = "v_users",
                        OracleDbType = OracleDbType.RefCursor,
                        Direction = ParameterDirection.Output
                    }
                }
            );

            // Transformamos los datos recibidos en un Enumerable
            result = data.AsEnumerable().Select(
                row => new User
                {
                    Id = Convert.ToInt32(row["id"]),
                    Role = new Role
                    {
                        Id = Convert.ToInt32(row["role_id"]),
                        Name = row["role_name"].ToString()
                    },
                    Name = row["name"].ToString(),
                    Email = row["email"].ToString()
                }
            );

            // Lo devolvemos
            return result;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            User user = new User();
            DataRow firstRow;

            // Ejecutamos el procedimiento en la BD
            var data = await _connection.ExecuteProcedure(
                spName: "proc_users_get_by_id",
                parameters: new List<OracleParameter>()
                {
                    new OracleParameter
                    {
                        ParameterName = "user_id",
                        OracleDbType = OracleDbType.Int32,
                        Direction = ParameterDirection.Input,
                        Value = id
                    },
                    new OracleParameter
                    {
                        ParameterName = "v_users",
                        OracleDbType = OracleDbType.RefCursor,
                        Direction = ParameterDirection.Output
                    }
                }
            );

            // Transformamos los datos recibidos en un Enumerable
            if (data.Rows.Count > 0)
            {
                firstRow = data.AsEnumerable().First();

                user.Id = Convert.ToInt32(firstRow["id"]);
                user.Role = new Role
                {
                    Id = Convert.ToInt32(firstRow["role_id"]),
                    Name = firstRow["role_name"].ToString()
                };
                user.Name = firstRow["name"].ToString();
                user.Email = firstRow["email"].ToString();
            }

            return user;
        }

        public async Task<bool> UpdateAsync(int id, User entity)
        {
            // TODO
            return false;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            // TODO
            return false;
        }
    }
}