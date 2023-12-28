using System.Data;
using Inventory.Entities;
using Inventory.Persistence.Interfaces;
using Oracle.ManagedDataAccess.Client;

namespace Inventory.Persistence.Repositories
{
    public class InventoryStockRepository : BaseRepository, IInventoryStockRepository
    {
        public async Task<InventoryStock?> CreateAsync(InventoryStock entity)
        {
            InventoryStock? inventoryStock = null;
            
            // Ejecutamos el procedimiento
            var data = await _connection.ExecuteProcedure(
                spName: "proc_inventory_stocks_create",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("new_product_id", OracleDbType.Int32, ParameterDirection.Input, entity.Product.Id),
                    _connection.AddParameter("new_quantity", OracleDbType.Int32, ParameterDirection.Input, entity.Quantity),
                    _connection.AddParameter("v_inventory_stock", OracleDbType.RefCursor, ParameterDirection.Output)
                }
            );
        
            // Obetenemos el enumerable
            if (data.Rows.Count > 0)
            {
                inventoryStock = new InventoryStock
                {
                    Id              = Convert.ToInt32(data.Rows[0]["id"]),
                    Product         = new Product
                    {
                        Id          = Convert.ToInt32(data.Rows[0]["product_id"]),
                        Name        = data.Rows[0]["product_name"].ToString(),
                        Description = data.Rows[0]["product_description"].ToString()
                    },
                    Quantity        = Convert.ToInt32(data.Rows[0]["quantity"])
                };
            }

            return inventoryStock;
        }

        public async Task<List<InventoryStock>> GetAllAsync()
        {
            // Ejecutamos el procedimiento
            var data = await _connection.ExecuteProcedure(
                spName: "proc_inventory_stocks_get_all",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("v_inventory_stocks", OracleDbType.RefCursor, ParameterDirection.Output)
                }
            );

            // Obtenemos la lista
            List<InventoryStock> result = new List<InventoryStock>();

            foreach (DataRow row in data.Rows)
            {
                result.Add(new InventoryStock
                {
                    Id              = Convert.ToInt32(row["id"]),
                    Product         = new Product
                    {
                        Id          = Convert.ToInt32(row["product_id"]),
                        Name        = row["product_name"].ToString(),
                        Description = data.Rows[0]["product_description"].ToString()
                    },
                    Quantity        = Convert.ToInt32(row["quantity"])
                });
            }

            return result;
        }

        public async Task<InventoryStock?> GetByIdAsync(int productId)
        {
            InventoryStock? inventoryStock = null;

            // Ejecutamos el procedimiento
            var data = await _connection.ExecuteProcedure(
                spName: "proc_inventory_stocks_get_by_product_id",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("inventory_stock_id", OracleDbType.Int32, ParameterDirection.Input, productId),
                    _connection.AddParameter("v_inventory_stock", OracleDbType.RefCursor, ParameterDirection.Output)
                }
            );

            // Obtenemos la entidad
            if (data.Rows.Count > 0)
            {
                inventoryStock = new InventoryStock
                {
                    Id              = Convert.ToInt32(data.Rows[0]["id"]),
                    Product         = new Product
                    {
                        Id          = Convert.ToInt32(data.Rows[0]["product_id"]),
                        Name        = data.Rows[0]["product_name"].ToString(),
                        Description = data.Rows[0]["product_description"].ToString()
                    },
                    Quantity        = Convert.ToInt32(data.Rows[0]["quantity"])
                };
            }

            return inventoryStock;
        }

        public async Task<bool> UpdateAsync(int productId, InventoryStock entity)
        {
            bool success = false; 

            // Ejecutamos el procedimiento
            var data = await _connection.ExecuteScalarProcedure(
                spName: "proc_inventory_stocks_update",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("product_id_to_update", OracleDbType.Int32, ParameterDirection.Input, productId),
                    _connection.AddParameter("new_quantity", OracleDbType.Int32, ParameterDirection.Input, entity.Quantity),
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
                spName: "proc_inventory_stocks_delete",
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