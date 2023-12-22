-- INVENTORY STOCKS CREATE
CREATE OR REPLACE PROCEDURE proc_inventory_stocks_create(
    new_product_id  IN inventory_stocks.product_id%TYPE,
    new_quantity    IN inventory_stocks.quantity%TYPE
)
AS
BEGIN
    INSERT INTO inventory_stocks
        (id, product_id, quantity, created_at)
    VALUES
        (0, new_product_id, new_quantity, SYSDATE);
END;
/

-- INVENTORY STOCKS GET ALL
CREATE OR REPLACE PROCEDURE proc_inventory_stocks_get_all(
    v_inventory_stocks  OUT SYS_REFCURSOR
)
AS
BEGIN
    OPEN v_inventory_stocks FOR
        SELECT
            ist.id, ist.product_id, p.name as product_name, ist.quantity        
        FROM inventory_stocks ist
            INNER JOIN products p ON ist.product_id = p.id;
END;
/

-- INVENTORY STOCKS GET BY ID
CREATE OR REPLACE PROCEDURE proc_inventory_stocks_get_by_id(
    inventory_stock_id  IN inventory_stocks.id%TYPE,
    v_inventory_stocks  OUT SYS_REFCURSOR
)
AS
BEGIN
    OPEN v_inventory_stocks FOR
        SELECT
            ist.id, ist.product_id, p.name as product_name, ist.quantity        
        FROM inventory_stocks ist
            INNER JOIN products p ON ist.product_id = p.id
        WHERE
            ist.id = inventory_stock_id;
END;
/

-- INVENTORY STOCKS UPDATE
CREATE OR REPLACE PROCEDURE proc_inventory_stocks_update(
    inventory_stock_id  IN inventory_stocks.id%TYPE,
    new_product_id      IN inventory_stocks.product_id%TYPE,
    new_quantity        IN inventory_stocks.quantity%TYPE
)
AS
BEGIN
    UPDATE inventory_stocks
    SET
        product_id  = new_product_id,
        quantity    = new_quantity,
        updated_at  = SYSDATE
    WHERE
        id          = inventory_stock_id;
END;
/

-- INVENTORY STOCKS DELETE
CREATE OR REPLACE PROCEDURE proc_inventory_stocks_delete(
    inventory_stock_id IN inventory_stocks.id%TYPE
)
AS
BEGIN
    DELETE FROM inventory_stocks
    WHERE
        id = inventory_stock_id;
END;
/