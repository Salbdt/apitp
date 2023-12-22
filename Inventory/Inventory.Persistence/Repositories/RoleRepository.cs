using System.Data;
using Inventory.Entities;
using Inventory.Persistence.Interfaces;
using Oracle.ManagedDataAccess.Client;

namespace Inventory.Persistence.Repositories
{
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        public RoleRepository()
        {
        }

        public async Task<Role> CreateAsync(Role entity)
        {
            // TODO
            var rol = new Role();
            return rol;
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {            
            IEnumerable<Role>? result = null;
            
            // Ejecutamos el procedimiento en la BD
            var data = await _connection.ExecuteProcedure(
                spName: "proc_roles_get_all",
                parameters: new List<OracleParameter>()
                {
                    new OracleParameter
                    {
                        ParameterName = "v_roles",
                        OracleDbType = OracleDbType.RefCursor,
                        Direction = ParameterDirection.Output
                    }
                }
            );

            // Transformamos los datos recibidos en un Enumerable
            result = data.AsEnumerable().Select(
                row => new Role
                {
                    Id = Convert.ToInt32(row["id"]),
                    Name = row["name"].ToString(),
                    Description = row["description"].ToString()
                }
            );

            // Lo devolvemos
            return result;
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            Role role = new Role();
            DataRow firstRow;

            // Ejecutamos el procedimiento en la BD
            var data = await _connection.ExecuteProcedure(
                spName: "proc_roles_get_by_id",
                parameters: new List<OracleParameter>()
                {
                    new OracleParameter
                    {
                        ParameterName = "role_id",
                        OracleDbType = OracleDbType.Int32,
                        Direction = ParameterDirection.Input,
                        Value = id
                    },
                    new OracleParameter
                    {
                        ParameterName = "v_roles",
                        OracleDbType = OracleDbType.RefCursor,
                        Direction = ParameterDirection.Output
                    }
                }
            );

            // Transformamos los datos recibidos en un Enumerable
            if (data.Rows.Count > 0)
            {
                firstRow = data.AsEnumerable().First();

                role.Id = Convert.ToInt32(firstRow["id"]);
                role.Name = firstRow["name"].ToString();
                role.Description = firstRow["description"].ToString();
            }

            return role;
        }

        public async Task<bool> UpdateAsync(int id, Role entity)
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