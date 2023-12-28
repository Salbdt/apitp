-- PRODUCTS GET ALL BY SELLER
CREATE OR REPLACE PROCEDURE proc_get_all_products_by_seller(
    user_id                 IN users.id%TYPE,
    v_products_by_seller    OUT SYS_REFCURSOR
)
AS
BEGIN
    OPEN v_products_by_seller FOR
        SELECT
            DISTINCT(p.id), p.name, p.description, p.price, p.category_id, c.name AS category_name, c.description AS category_description, 
            imo.user_id, u.name AS user_name
        FROM products p
            INNER JOIN categories c ON p.category_id = c.id
            INNER JOIN inventory_movements imo ON p.id = imo.product_id
            INNER JOIN users u ON imo.user_id = u.id
        WHERE u.id = user_id;
END;
/