-- INVENTORY STOCKS CREATE
CREATE OR REPLACE PROCEDURE proc_inventory_stocks_create(
    new_product_id      IN inventory_stocks.product_id%TYPE,
    new_quantity        IN inventory_stocks.quantity%TYPE,
    v_inventory_stock   OUT SYS_REFCURSOR
)
AS
v_inventory_stock_id   inventory_stocks.id%TYPE;
BEGIN
    INSERT INTO inventory_stocks (id, product_id, quantity, created_at)
    VALUES (0, new_product_id, new_quantity, SYSDATE)
    RETURNING id INTO v_inventory_stock_id;
    
    OPEN v_inventory_stock FOR
        SELECT ist.id, ist.product_id, p.name as product_name, p.description as product_description, ist.quantity        
        FROM inventory_stocks ist INNER JOIN products p ON ist.product_id = p.id
        WHERE ist.id = v_inventory_stock_id;
        
    COMMIT;    
END;
/

-- INVENTORY STOCKS GET ALL
CREATE OR REPLACE PROCEDURE proc_inventory_stocks_get_all(
    v_inventory_stocks  OUT SYS_REFCURSOR
)
AS
BEGIN
    OPEN v_inventory_stocks FOR
        SELECT ist.id, ist.product_id, p.name as product_name, p.description as product_description, ist.quantity        
        FROM inventory_stocks ist INNER JOIN products p ON ist.product_id = p.id;
END;
/

-- INVENTORY STOCKS GET BY ID
CREATE OR REPLACE PROCEDURE proc_inventory_stocks_get_by_product_id(
    product_id_to_get   IN inventory_stocks.product_id%TYPE,
    v_inventory_stock   OUT SYS_REFCURSOR
)
AS
BEGIN
    OPEN v_inventory_stock FOR
        SELECT ist.id, ist.product_id, p.name as product_name, p.description as product_description, ist.quantity        
        FROM inventory_stocks ist INNER JOIN products p ON ist.product_id = p.id
        WHERE ist.product_id = product_id_to_get;
END;
/

-- INVENTORY STOCKS UPDATE
CREATE OR REPLACE PROCEDURE proc_inventory_stocks_update(
    product_id_to_update    IN inventory_stocks.product_id%TYPE,
    new_quantity            IN inventory_stocks.quantity%TYPE,
    v_result                OUT NUMBER
)
AS
BEGIN
    UPDATE inventory_stocks SET
        quantity    = new_quantity,
        updated_at  = SYSDATE
    WHERE product_id  = product_id_to_update;
    
    v_result := SQL%ROWCOUNT;
    
    COMMIT;
END;
/

-- INVENTORY STOCKS DELETE
CREATE OR REPLACE PROCEDURE proc_inventory_stocks_delete(
    product_id_to_delete    IN inventory_stocks.product_id%TYPE,
    v_result                OUT NUMBER
)
AS
BEGIN
    DELETE FROM inventory_stocks
    WHERE product_id    = product_id_to_delete
    AND quantity        = 0;
    
    v_result := SQL%ROWCOUNT;
    
    COMMIT;
END;
/