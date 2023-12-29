using System.Data;
using Inventory.Entities;
using Inventory.Persistence.Interfaces;
using Oracle.ManagedDataAccess.Client;

namespace Inventory.Persistence.Repositories
{
    public class ReportRepository : DBRepository, IReportRepository
    {
        public async Task<List<Product>> GetAllProductsBySellerAsync(int userId)
        {
            // Ejecutamos el procedimiento
            var data = await _connection.ExecuteProcedure(
                spName: "proc_get_all_products_by_seller",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("user_id", OracleDbType.Int32, ParameterDirection.Output, userId),
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
    }
}