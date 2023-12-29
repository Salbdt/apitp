using System.Data;
using Inventory.Entities;
using Inventory.Persistence.Interfaces.CRUD;
using Oracle.ManagedDataAccess.Client;

namespace Inventory.Persistence.Repositories.CRUD
{
    public class CategoryRepository : DBRepository, ICategoryRepository
    {
        public async Task<Category?> CreateAsync(Category entity)
        {
            Category? category = null;
            
            // Ejecutamos el procedimiento
            var data = await _connection.ExecuteProcedure(
                spName: "proc_categories_create",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("new_name", OracleDbType.Varchar2, ParameterDirection.Input, entity.Name),
                    _connection.AddParameter("new_description", OracleDbType.Varchar2, ParameterDirection.Input, entity.Description),
                    _connection.AddParameter("v_category", OracleDbType.RefCursor, ParameterDirection.Output)
                }
            );
        
            // Obetenemos el enumerable
            if (data.Rows.Count > 0)
            {
                category = new Category
                {
                    Id          = Convert.ToInt32(data.Rows[0]["id"]),
                    Name        = data.Rows[0]["name"].ToString(),
                    Description = data.Rows[0]["description"].ToString()
                };
            }

            return category;
        }

        public async Task<List<Category>> GetAllAsync()
        {            
            // Ejecutamos el procedimiento
            var data = await _connection.ExecuteProcedure(
                spName: "proc_categories_get_all",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("v_categories", OracleDbType.RefCursor, ParameterDirection.Output)
                }
            );

            // Obtenemos la lista
            List<Category> result = new List<Category>();

            foreach (DataRow row in data.Rows)
            {
                result.Add(new Category
                {
                    Id          = Convert.ToInt32(row["id"]),
                    Name        = row["name"].ToString(),
                    Description = row["description"].ToString()
                });
            }

            return result;
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            Category? category = null;

            // Ejecutamos el procedimiento
            var data = await _connection.ExecuteProcedure(
                spName: "proc_categories_get_by_id",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("category_id", OracleDbType.Int32, ParameterDirection.Input, id),
                    _connection.AddParameter("v_category", OracleDbType.RefCursor, ParameterDirection.Output)
                }
            );

            // Obtenemos la entidad
            if (data.Rows.Count > 0)
            {
                category = new Category
                {
                    Id              = Convert.ToInt32(data.Rows[0]["id"]),
                    Name            = data.Rows[0]["name"].ToString(),
                    Description     = data.Rows[0]["description"].ToString()
                };
            }

            return category;
        }

        public async Task<bool> UpdateAsync(int id, Category entity)
        {            
            bool success = false; 

            // Ejecutamos el procedimiento
            var data = await _connection.ExecuteScalarProcedure(
                spName: "proc_categories_update",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("category_id", OracleDbType.Int32, ParameterDirection.Input, id),
                    _connection.AddParameter("new_name", OracleDbType.Varchar2, ParameterDirection.Input, entity.Name),
                    _connection.AddParameter("new_description", OracleDbType.Varchar2, ParameterDirection.Input, entity.Description),
                    _connection.AddParameter("v_result", OracleDbType.Int32, ParameterDirection.Output)
                }
            );

            // Obtenemos el valor
            if (data > 0)
                success = true;

            return success;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool success = false;   

            // Ejecutamos el procedimiento
            var data = await _connection.ExecuteScalarProcedure(
                spName: "proc_categories_delete",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("category_id_to_delete", OracleDbType.Int32, ParameterDirection.Input, id),
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