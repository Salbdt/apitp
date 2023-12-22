-- PRODUCTS CREATE
CREATE OR REPLACE PROCEDURE proc_products_create(
    new_category_id IN products.category_id%TYPE,
    new_name        IN products.name%TYPE,
    new_description IN products.description%TYPE,
    new_price       IN products.price%TYPE
)
AS
BEGIN
    INSERT INTO products
        (id, category_id, name, description, price, created_at)
    VALUES
        (0, new_category_id, new_name, new_description, new_price, SYSDATE);
END;
/

-- PRODUCTS GET ALL
CREATE OR REPLACE PROCEDURE proc_products_get_all(
    v_products  OUT SYS_REFCURSOR
)
AS
BEGIN
    OPEN v_products FOR
        SELECT
            p.id, p.category_id, c.name as category_name, p.name, p.description, p.price
        FROM products p
            INNER JOIN categories c ON p.category_id = c.id;
END;
/

-- PRODUCTS GET BY ID
CREATE OR REPLACE PROCEDURE proc_products_get_by_id(
    product_id  IN products.id%TYPE,
    v_products  OUT SYS_REFCURSOR
)
AS
BEGIN
    OPEN v_products FOR
        SELECT
            p.id, p.category_id, c.name as category_name, p.name, p.description, p.price
        FROM products p
            INNER JOIN categories c ON p.category_id = c.id
        WHERE
            p.id = product_id;
END;
/

-- PRODUCTS UPDATE
CREATE OR REPLACE PROCEDURE proc_products_update(
    product_id      IN products.id%TYPE,
    new_category_id IN products.category_id%TYPE,
    new_name        IN products.name%TYPE,
    new_description IN products.description%TYPE,
    new_price       IN products.price%TYPE 
)
AS
BEGIN
    UPDATE products
    SET
        category_id = new_category_id,
        name        = new_name,
        description = new_description,
        price       = new_price,
        updated_at  = SYSDATE
    WHERE
        id          = product_id;
END;
/

-- PRODUCTS DELETE
CREATE OR REPLACE PROCEDURE proc_products_delete(
    product_id IN products.id%TYPE
)
AS
BEGIN
    DELETE FROM products
    WHERE
        id = product_id;
END;
/