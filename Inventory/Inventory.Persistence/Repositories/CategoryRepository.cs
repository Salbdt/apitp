using System.Data;
using Inventory.Entities;
using Inventory.Persistence.Interfaces;
using Oracle.ManagedDataAccess.Client;

namespace Inventory.Persistence.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
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
                DataRow firstRow = data.AsEnumerable().First();

                category = new Category
                {
                    Id          = Convert.ToInt32(firstRow["id"]),
                    Name        = firstRow["name"].ToString(),
                    Description = firstRow["description"].ToString()
                };
            }

            return category;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {            
            IEnumerable<Category>? result = null;
            
            // Ejecutamos el procedimiento
            var data = await _connection.ExecuteProcedure(
                spName: "proc_categories_get_all",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("v_categories", OracleDbType.RefCursor, ParameterDirection.Output)
                }
            );

            // Obtenemos el enumerable
            result = data.AsEnumerable().Select(
                row => new Category
                {
                    Id          = Convert.ToInt32(row["id"]),
                    Name        = row["name"].ToString(),
                    Description = row["description"].ToString()
                }
            );

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

            // Obtenemos el enumerable
            if (data.Rows.Count > 0)
            {
                DataRow firstRow = data.AsEnumerable().First();

                category = new Category
                {
                    Id              = Convert.ToInt32(firstRow["id"]),
                    Name            = firstRow["name"].ToString(),
                    Description     = firstRow["description"].ToString()
                };
            }

            return category;
        }

        public async Task<bool> UpdateAsync(int id, Category entity)
        {            
            bool success = false; 

            // Ejecutamos el procedimiento
            var data = await _connection.ExecuteProcedure(
                spName: "proc_categories_update",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("category_id", OracleDbType.Int32, ParameterDirection.Input, id),
                    _connection.AddParameter("new_name", OracleDbType.Varchar2, ParameterDirection.Input, entity.Name),
                    _connection.AddParameter("new_description", OracleDbType.Varchar2, ParameterDirection.Input, entity.Description),
                    _connection.AddParameter("v_category", OracleDbType.RefCursor, ParameterDirection.Output)
                }
            );

            // Obtenemos el enumerable
            if (data.Rows.Count > 0)
                success = true;

            return success;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool success = false;   

            // Ejecutamos el procedimiento
            var data = await _connection.ExecuteProcedure(
                spName: "proc_categories_delete",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("category_id", OracleDbType.Int32, ParameterDirection.Input, id),
                    _connection.AddParameter("v_category", OracleDbType.RefCursor, ParameterDirection.Output)
                }
            );

            // Obtenemos el enumerable
            if (data.Rows.Count > 0)
                success = true;

            return success;
        }
    }
}