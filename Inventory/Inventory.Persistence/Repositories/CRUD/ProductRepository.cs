using System.Data;
using Inventory.Entities;
using Inventory.Persistence.Interfaces.CRUD;
using Oracle.ManagedDataAccess.Client;

namespace Inventory.Persistence.Repositories.CRUD
{
    public class ProductRepository : DBRepository, IProductRepository
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
                product = new Product
                {
                    Id              = Convert.ToInt32(data.Rows[0]["id"]),
                    Category        = new Category
                    {
                        Id          = Convert.ToInt32(data.Rows[0]["category_id"]),
                        Name        = data.Rows[0]["category_name"].ToString(),
                        Description = data.Rows[0]["category_description"].ToString()
                    },
                    Name            = data.Rows[0]["name"].ToString(),
                    Description     = data.Rows[0]["description"].ToString(),
                    Price           = Convert.ToDecimal(data.Rows[0]["price"])
                };
            }

            return product;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            // Ejecutamos el procedimiento
            var data = await _connection.ExecuteProcedure(
                spName: "proc_products_get_all",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("v_products", OracleDbType.RefCursor, ParameterDirection.Output)
                }
            );

            // Obtenemos la lista
            List<Product> result = new List<Product>();

            foreach (DataRow row in data.Rows)
            {
                result.Add(new Product
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
                });
            }

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

            // Obtenemos la entidad
            if (data.Rows.Count > 0)
            {
                product = new Product
                {
                    Id              = Convert.ToInt32(data.Rows[0]["id"]),
                    Category        = new Category
                    {
                        Id          = Convert.ToInt32(data.Rows[0]["category_id"]),
                        Name        = data.Rows[0]["category_name"].ToString(),
                        Description = data.Rows[0]["category_description"].ToString()
                    },
                    Name            = data.Rows[0]["name"].ToString(),
                    Description     = data.Rows[0]["description"].ToString(),
                    Price           = Convert.ToDecimal(data.Rows[0]["price"])
                };
            }

            return product;
        }

        public async Task<bool> UpdateAsync(int id, Product entity)
        {
            bool success = false; 

            // Ejecutamos el procedimiento
            var data = await _connection.ExecuteScalarProcedure(
                spName: "proc_products_update",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("product_id", OracleDbType.Int32, ParameterDirection.Input, id),
                    _connection.AddParameter("new_category_id", OracleDbType.Int32, ParameterDirection.Input, entity.Category.Id),
                    _connection.AddParameter("new_name", OracleDbType.Varchar2, ParameterDirection.Input, entity.Name),
                    _connection.AddParameter("new_description", OracleDbType.Varchar2, ParameterDirection.Input, entity.Description),
                    _connection.AddParameter("new_price", OracleDbType.Decimal, ParameterDirection.Input, entity.Price),
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
                spName: "proc_products_delete",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("product_id_to_delete", OracleDbType.Int32, ParameterDirection.Input, id),
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