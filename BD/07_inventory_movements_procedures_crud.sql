-- INVENTORY MOVEMENTS CREATE
CREATE OR REPLACE PROCEDURE proc_inventory_movements_create(
    new_product_id          IN inventory_movements.product_id%TYPE,
    new_user_id             IN inventory_movements.user_id%TYPE,
    new_quantity            IN inventory_movements.quantity%TYPE,
    new_movement_type       IN inventory_movements.movement_type%TYPE,
    v_inventory_movement    OUT SYS_REFCURSOR
)
AS
v_inventory_movement_id inventory_movements.id%TYPE;
v_stock_quantity        inventory_stocks.quantity%TYPE;
v_result                NUMBER;
BEGIN
    -- Insertamos el movimiento
    INSERT INTO inventory_movements (id, product_id, user_id, quantity, movement_type, movement_date)
    VALUES (0, new_product_id, new_user_id, new_quantity, new_movement_type, SYSDATE)
    RETURNING id INTO v_inventory_movement_id;
    
    -- Las posibilidades son COMPRA para comprar productos y VENTA para vender productos
    IF (new_movement_type = 'COMPRA') THEN    
        proc_inventory_stocks_update(new_product_id, new_quantity, v_result);             -- Sumamos
    ELSE
        IF (new_movement_type = 'VENTA') THEN
            proc_inventory_stocks_update(new_product_id, new_quantity * -1, v_result);    -- Restamos
        END IF;
    END IF;
        
    SELECT ist.quantity INTO v_stock_quantity
    FROM inventory_stocks ist
    WHERE ist.product_id = new_product_id;
        
    if (v_stock_quantity < 0) THEN
        ROLLBACK;
    ELSE        
        COMMIT;
    END IF;
    
    OPEN v_inventory_movement FOR    
        SELECT im.id, im.product_id, p.name AS product_name, p.description AS product_description, im.user_id, u.name AS user_name, im.quantity, im.movement_type, im.movement_date
        FROM inventory_movements im
            INNER JOIN products p ON im.product_id = p.id
            INNER JOIN users u ON im.user_id = u.id
        WHERE im.id = v_inventory_movement_id;
END;
/

-- INVENTORY MOVEMENTS GET ALL
CREATE OR REPLACE PROCEDURE proc_inventory_movements_get_all(
    v_inventory_movements   OUT SYS_REFCURSOR
)
AS
BEGIN
    OPEN v_inventory_movements FOR    
        SELECT im.id, im.product_id, p.name AS product_name, p.description as product_description, im.user_id, u.name AS user_name, im.quantity, im.movement_type, im.movement_date
        FROM inventory_movements im
            INNER JOIN products p ON im.product_id = p.id
            INNER JOIN users u ON im.user_id = u.id;
END;
/

-- INVENTORY MOVEMENTS GET BY ID
CREATE OR REPLACE PROCEDURE proc_inventory_movements_get_by_id(
    inventory_movement_id IN inventory_movements.id%TYPE,
    v_inventory_movement   OUT SYS_REFCURSOR
)
AS
BEGIN
    OPEN v_inventory_movement FOR    
        SELECT im.id, im.product_id, p.name AS product_name, p.description as product_description, im.user_id, u.name AS user_name, im.quantity, im.movement_type, im.movement_date
        FROM inventory_movements im
            INNER JOIN products p ON im.product_id = p.id
            INNER JOIN users u ON im.user_id = u.id
        WHERE im.id = inventory_movement_id;
END;
/

-- INVENTORY MOVEMENTS UPDATE
CREATE OR REPLACE PROCEDURE proc_inventory_movements_update(
    inventory_movement_id   IN inventory_movements.id%TYPE,
    new_product_id          IN inventory_movements.product_id%TYPE,
    new_user_id             IN inventory_movements.user_id%TYPE,
    new_quantity            IN inventory_movements.quantity%TYPE,
    new_movement_type       IN inventory_movements.movement_type%TYPE,
    v_result                OUT NUMBER
)
AS
BEGIN
    UPDATE inventory_movements SET
        product_id      = new_product_id,
        user_id         = new_user_id,
        quantity        = new_quantity,
        movement_type   = new_movement_type
    WHERE id            = inventory_movement_id;
    
    v_result := SQL%ROWCOUNT;
    
    COMMIT;
END;
/

-- INVENTORY MOVEMENTS DELETE
CREATE OR REPLACE PROCEDURE proc_inventory_movements_delete(
    inventory_movement_id IN inventory_movements.id%TYPE,
    v_result                OUT NUMBER
)
AS
BEGIN
    DELETE FROM inventory_movements
    WHERE id = inventory_movement_id;
    
    v_result := SQL%ROWCOUNT;
    
    COMMIT;
END;
/