using System.Data;
using Inventory.Entities;
using Inventory.Persistence.Interfaces.CRUD;
using Oracle.ManagedDataAccess.Client;

namespace Inventory.Persistence.Repositories.CRUD
{
    public class RoleRepository : DBRepository, IRoleRepository
    {
        public async Task<Role?> CreateAsync(Role entity)
        { // No implementado
            return null;
        }

        public async Task<List<Role>> GetAllAsync()
        {
            // Ejecutamos el procedimiento
            var data = await _connection.ExecuteProcedure(
                spName: "proc_roles_get_all",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("v_roles", OracleDbType.RefCursor, ParameterDirection.Output)
                }
            );

            // Obtenemos la lista
            List<Role> result = new List<Role>();

            foreach (DataRow row in data.Rows)
            {
                result.Add(new Role
                {
                    Id          = Convert.ToInt32(row["id"]),
                    Name        = row["name"].ToString(),
                    Description = row["description"].ToString()
                });
            }

            return result;
        }

        public async Task<Role?> GetByIdAsync(int id)
        {
            Role? role = null;

            // Ejecutamos el procedimiento
            var data = await _connection.ExecuteProcedure(
                spName: "proc_roles_get_by_id",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("role_id", OracleDbType.Int32, ParameterDirection.Input, id),
                    _connection.AddParameter("v_role", OracleDbType.RefCursor, ParameterDirection.Output)
                }
            );

            // Obtenemos la entidad
            if (data.Rows.Count > 0)
            {
                role = new Role
                {
                    Id             = Convert.ToInt32(data.Rows[0]["id"]),
                    Name           = data.Rows[0]["name"].ToString(),
                    Description    = data.Rows[0]["description"].ToString(),
                };
            }

            return role;
        }

        public async Task<bool> UpdateAsync(int id, Role entity)
        { // No implementado
            return false;
        }
        public async Task<bool> DeleteAsync(int id)
        { // No implementado
            return false;
        }
    }
}