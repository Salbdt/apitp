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
            return null;
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {            
            IEnumerable<Role>? result = null;
            
            // Ejecutamos el procedimiento en la BD
            var data = await _connection.ExecuteProcedure(
                spName: "proc_roles_get_all",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("v_roles", OracleDbType.RefCursor, ParameterDirection.Output)
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
                    _connection.AddParameter("role_id", OracleDbType.Int32, ParameterDirection.Input, id),
                    _connection.AddParameter("v_roles", OracleDbType.RefCursor, ParameterDirection.Output)
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
            return false;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return false;
        }
    }
}