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
            Console.WriteLine($"User ID Parámetro: {userId}");
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
            Console.WriteLine($"User ID DB: {data.Rows[0]["user_id"]}");
            Console.WriteLine($"Productos: {data.Rows.Count}");
            Console.WriteLine("\nROWS:");
            foreach (DataRow row in data.Rows)
            {
                Console.WriteLine($"Row ID: {row["id"]}");
                result.Add(new Product
                {
                    Id              = Convert.ToInt32(row["id"]),
                    Name            = row["name"].ToString(),
                    Description     = row["description"].ToString(),
                    Price           = Convert.ToDecimal(row["price"])
                });
            }

            return result;
        }
    }
}