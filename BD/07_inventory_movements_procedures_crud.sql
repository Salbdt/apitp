-- INVENTORY MOVEMENTS CREATE
CREATE OR REPLACE PROCEDURE proc_inventory_movements_create(
    new_product_id      IN inventory_movements.product_id%TYPE,
    new_user_id         IN inventory_movements.user_id%TYPE,
    new_quantity        IN inventory_movements.quantity%TYPE,
    new_movement_type   IN inventory_movements.movement_type%TYPE
)
AS
v_inventory_stock_id    inventory_stocks.id%TYPE;
v_new_quantity          inventory_movements.quantity%TYPE;
BEGIN
    -- Para trabajar siempre cON números positivos
    IF (new_quantity < 0) then
        select 
            new_quantity * -1
        INTO v_new_quantity
        FROM dual;
    END IF;

    INSERT INTO inventory_movements
        (id, product_id, user_id, quantity, movement_type, movement_date)
    VALUES
        (0, new_product_id, new_user_id, v_new_quantity, new_movement_type, SYSDATE);
    
    SELECT id INTO v_inventory_stock_id
    FROM inventory_stocks
    WHERE
        product_id = new_product_id;
    
    -- LAS posibilidades sON INGRESO para comprar productos y EGRESO para vender productos
    IF (new_movement_type = 'INGRESO') then    
        proc_inventory_stocks_update(v_inventory_stock_id, new_product_id, v_new_quantity);
    ELSE
        IF (new_movement_type = 'EGRESO') THEN
            proc_inventory_stocks_update(v_inventory_stock_id, new_product_id, v_new_quantity * -1);
        END IF;
    END IF;
END;
/

-- INVENTORY MOVEMENTS GET ALL
CREATE OR REPLACE PROCEDURE proc_inventory_movements_get_all(
    v_inventory_movements   OUT SYS_REFCURSOR
)
AS
BEGIN
    OPEN v_inventory_movements FOR    
        SELECT
            im.id, im.product_id, p.name AS product_name, im.user_id, u.name AS user_name, im.quantity, im.movement_type
        FROM inventory_movements im
            INNER JOIN products p ON im.product_id = p.id
            INNER JOIN users u ON im.user_id = u.id;
END;
/

-- INVENTORY MOVEMENTS GET BY ID
CREATE OR REPLACE PROCEDURE proc_inventory_movements_get_by_id(
    inventory_movement_id IN inventory_movements.id%TYPE,
    v_inventory_movements   OUT SYS_REFCURSOR
)
AS
BEGIN
    OPEN v_inventory_movements FOR    
        SELECT
            im.id, im.product_id, p.name AS product_name, im.user_id, u.name AS user_name, im.quantity, im.movement_type
        FROM inventory_movements im
            INNER JOIN products p ON im.product_id = p.id
            INNER JOIN users u ON im.user_id = u.id
            WHERE
                im.id = inventory_movement_id;
END;
/

-- INVENTORY MOVEMENTS UPDATE
CREATE OR REPLACE PROCEDURE proc_inventory_movements_update(
    inventory_movement_id   IN inventory_movements.id%TYPE,
    new_product_id          IN inventory_movements.product_id%TYPE,
    new_user_id             IN inventory_movements.user_id%TYPE,
    new_quantity            IN inventory_movements.quantity%TYPE,
    new_movement_type       IN inventory_movements.movement_type%TYPE
)
AS
BEGIN
    update inventory_movements
    set
        product_id      = new_product_id,
        user_id         = new_user_id,
        quantity        = new_quantity,
        movement_type   = new_movement_type
    WHERE
        id              = inventory_movement_id;
END;
/

-- INVENTORY MOVEMENTS DELETE
CREATE OR REPLACE PROCEDURE proc_inventory_movements_delete(
    inventory_movement_id IN inventory_movements.id%TYPE
)
AS
BEGIN
    DELETE FROM inventory_movements
    WHERE
        id = inventory_movement_id;
END;
/