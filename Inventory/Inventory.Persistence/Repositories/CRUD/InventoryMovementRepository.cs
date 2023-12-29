using System.Data;
using Inventory.Entities;
using Inventory.Persistence.Interfaces.CRUD;
using Oracle.ManagedDataAccess.Client;

namespace Inventory.Persistence.Repositories.CRUD
{
    public class InventoryMovementRepository : DBRepository, IInventoryMovementRepository
    {
        public async Task<InventoryMovement?> CreateAsync(InventoryMovement entity)
        {
            InventoryMovement? inventoryMovement = null;
            
            // Ejecutamos el procedimiento
            var data = await _connection.ExecuteProcedure(
                spName: "proc_inventory_movements_create",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("new_product_id", OracleDbType.Int32, ParameterDirection.Input, entity.Product.Id),
                    _connection.AddParameter("new_user_id", OracleDbType.Int32, ParameterDirection.Input, entity.User.Id),
                    _connection.AddParameter("new_quantity", OracleDbType.Int32, ParameterDirection.Input, entity.Quantity),
                    _connection.AddParameter("new_movement_type", OracleDbType.Varchar2, ParameterDirection.Input, entity.MovementType.ToString()),
                    _connection.AddParameter("v_inventory_movement", OracleDbType.RefCursor, ParameterDirection.Output)
                }
            );
        
            // Obetenemos el enumerable
            if (data.Rows.Count > 0)
            {
                inventoryMovement = new InventoryMovement
                {
                    Id              = Convert.ToInt32(data.Rows[0]["id"]),
                    Product         = new Product
                    {
                        Id          = Convert.ToInt32(data.Rows[0]["product_id"]),
                        Name        = data.Rows[0]["product_name"].ToString(),
                        Description = data.Rows[0]["product_description"].ToString()
                    },
                    User            = new User
                    {
                        Id          = Convert.ToInt32(data.Rows[0]["user_id"]),
                        Name        = data.Rows[0]["user_name"].ToString()
                    },
                    Quantity        = Convert.ToInt32(data.Rows[0]["quantity"]),
                    MovementType    = InventoryMovement.GetMovementType(data.Rows[0]["movement_type"].ToString()),
                    MovementDate    = DateTime.Parse(data.Rows[0]["movement_date"].ToString())
                };
            }

            return inventoryMovement;
        }

        public async Task<List<InventoryMovement>> GetAllAsync()
        {
            // Ejecutamos el procedimiento
            var data = await _connection.ExecuteProcedure(
                spName: "proc_inventory_movements_get_all",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("v_inventory_movements", OracleDbType.RefCursor, ParameterDirection.Output)
                }
            );

            // Obtenemos la lista
            List<InventoryMovement> result = new List<InventoryMovement>();

            foreach (DataRow row in data.Rows)
            {
                result.Add(new InventoryMovement
                {
                    Id              = Convert.ToInt32(row["id"]),
                    Product         = new Product
                    {
                        Id          = Convert.ToInt32(row["product_id"]),
                        Name        = row["product_name"].ToString(),
                        Description = data.Rows[0]["product_description"].ToString()
                    },
                    User            = new User
                    {
                        Id          = Convert.ToInt32(row["user_id"]),
                        Name        = row["user_name"].ToString()
                    },
                    Quantity        = Convert.ToInt32(row["quantity"]),                    
                    MovementType    = InventoryMovement.GetMovementType(row["movement_type"].ToString()),
                    MovementDate    = DateTime.Parse(row["movement_date"].ToString())
                });
            }

            return result;
        }

        public async Task<InventoryMovement?> GetByIdAsync(int id)
        {
            InventoryMovement? inventoryMovement = null;

            // Ejecutamos el procedimiento
            var data = await _connection.ExecuteProcedure(
                spName: "proc_inventory_movements_get_by_id",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("inventory_movement_id", OracleDbType.Int32, ParameterDirection.Input, id),
                    _connection.AddParameter("v_inventory_movement", OracleDbType.RefCursor, ParameterDirection.Output)
                }
            );

            // Obtenemos la entidad
            if (data.Rows.Count > 0)
            {
                inventoryMovement = new InventoryMovement
                {
                    Id              = Convert.ToInt32(data.Rows[0]["id"]),
                    Product         = new Product
                    {
                        Id          = Convert.ToInt32(data.Rows[0]["product_id"]),
                        Name        = data.Rows[0]["product_name"].ToString(),
                        Description = data.Rows[0]["product_description"].ToString()
                    },
                    User            = new User
                    {
                        Id          = Convert.ToInt32(data.Rows[0]["user_id"]),
                        Name        = data.Rows[0]["user_name"].ToString()
                    },
                    Quantity        = Convert.ToInt32(data.Rows[0]["quantity"]),                    
                    MovementType    = InventoryMovement.GetMovementType(data.Rows[0]["movement_type"].ToString()),
                    MovementDate    = DateTime.Parse(data.Rows[0]["movement_date"].ToString())
                };
            }

            return inventoryMovement;
        }

        public async Task<bool> UpdateAsync(int id, InventoryMovement entity)
        {
            bool success = false; 

            // Ejecutamos el procedimiento
            var data = await _connection.ExecuteScalarProcedure(
                spName: "proc_inventory_movements_update",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("inventory_movement_id", OracleDbType.Int32, ParameterDirection.Input, id),
                    _connection.AddParameter("new_product_id", OracleDbType.Int32, ParameterDirection.Input, entity.Product.Id),
                    _connection.AddParameter("new_user_id", OracleDbType.Int32, ParameterDirection.Input, entity.User.Id),
                    _connection.AddParameter("new_quantity", OracleDbType.Int32, ParameterDirection.Input, entity.Quantity),
                    _connection.AddParameter("new_movement_type", OracleDbType.Varchar2, ParameterDirection.Input, entity.MovementType.ToString()),
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
                spName: "proc_inventory_movements_delete",
                parameters: new List<OracleParameter>()
                {
                    _connection.AddParameter("inventory_movement_id", OracleDbType.Int32, ParameterDirection.Input, id),
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