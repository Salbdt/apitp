using System.Data;
using Inventory.Entities;
using Inventory.Persistence.Interfaces;
using Oracle.ManagedDataAccess.Client;

namespace Inventory.Persistence.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public async Task<Product?> CreateAsync(Product entity)
        {
            Product? product = null;
            
            // Ejecutamos el procedimiento
            var data = await _connection.ExecuteProcedure(
                spName: "proc_products_create",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("new_category_id", OracleDbType.Int32, ParameterDirection.Input, entity.Category.Id),
                    _connection.AddParameter("new_name", OracleDbType.Varchar2, ParameterDirection.Input, entity.Name),
                    _connection.AddParameter("new_description", OracleDbType.Varchar2, ParameterDirection.Input, entity.Description),
                    _connection.AddParameter("new_price", OracleDbType.Decimal, ParameterDirection.Input, entity.Price),
                    _connection.AddParameter("v_product", OracleDbType.RefCursor, ParameterDirection.Output)
                }
            );
        
            // Obetenemos el enumerable
            if (data.Rows.Count > 0)
            {
                DataRow firstRow = data.AsEnumerable().First();

                product = new Product
                {
                    Id              = Convert.ToInt32(firstRow["id"]),
                    Category        = new Category
                    {
                        Id          = Convert.ToInt32(firstRow["category_id"]),
                        Name        = firstRow["category_name"].ToString(),
                        Description = firstRow["category_description"].ToString()
                    },
                    Name            = firstRow["name"].ToString(),
                    Description     = firstRow["description"].ToString(),
                    Price           = Convert.ToDecimal(firstRow["price"])
                };
            }

            return product;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {            
            IEnumerable<Product>? result = null;
            
            // Ejecutamos el procedimiento
            var data = await _connection.ExecuteProcedure(
                spName: "proc_products_get_all",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("v_products", OracleDbType.RefCursor, ParameterDirection.Output)
                }
            );

            // Obtenemos el enumerable
            result = data.AsEnumerable().Select(
                row => new Product
                {
                    Id              = Convert.ToInt32(row["id"]),
                    Category        = new Category
                    {
                        Id          = Convert.ToInt32(row["category_id"]),
                        Name        = row["category_name"].ToString(),
                        Description = row["category_description"].ToString()
                    },
                    Name            = row["name"].ToString(),
                    Description     = row["description"].ToString(),
                    Price           = Convert.ToDecimal(row["price"])
                }
            );

            return result;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            Product? product = null;

            // Ejecutamos el procedimiento
            var data = await _connection.ExecuteProcedure(
                spName: "proc_products_get_by_id",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("product_id", OracleDbType.Int32, ParameterDirection.Input, id),
                    _connection.AddParameter("v_product", OracleDbType.RefCursor, ParameterDirection.Output)
                }
            );

            // Obtenemos el enumerable
            if (data.Rows.Count > 0)
            {
                DataRow firstRow = data.AsEnumerable().First();

                product = new Product
                {
                    Id              = Convert.ToInt32(firstRow["id"]),
                    Category        = new Category
                    {
                        Id          = Convert.ToInt32(firstRow["category_id"]),
                        Name        = firstRow["category_name"].ToString(),
                        Description = firstRow["category_description"].ToString()
                    },
                    Name            = firstRow["name"].ToString(),
                    Description     = firstRow["description"].ToString(),
                    Price           = Convert.ToDecimal(firstRow["price"])
                };
            }

            return product;
        }

        public async Task<bool> UpdateAsync(int id, Product entity)
        {
            bool success = false; 

            // Ejecutamos el procedimiento
            var data = await _connection.ExecuteProcedure(
                spName: "proc_products_update",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("product_id", OracleDbType.Int32, ParameterDirection.Input, id),
                    _connection.AddParameter("new_category_id", OracleDbType.Int32, ParameterDirection.Input, entity.Category.Id),
                    _connection.AddParameter("new_name", OracleDbType.Varchar2, ParameterDirection.Input, entity.Name),
                    _connection.AddParameter("new_description", OracleDbType.Varchar2, ParameterDirection.Input, entity.Description),
                    _connection.AddParameter("new_price", OracleDbType.Decimal, ParameterDirection.Input, entity.Price),
                    _connection.AddParameter("v_product", OracleDbType.RefCursor, ParameterDirection.Output)
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
                spName: "proc_products_delete",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("product_id", OracleDbType.Int32, ParameterDirection.Input, id),
                    _connection.AddParameter("v_product", OracleDbType.RefCursor, ParameterDirection.Output)
                }
            );

            // Obtenemos el enumerable
            if (data.Rows.Count > 0)
                success = true;

            return success;
        }        
    }
}