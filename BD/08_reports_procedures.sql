-- PRODUCTS GET ALL BY SELLER
CREATE OR REPLACE PROCEDURE proc_get_all_products_by_seller(
    user_id     IN users.id%TYPE,
    v_products  OUT SYS_REFCURSOR
)
AS
BEGIN
    OPEN v_products FOR
        SELECT -- El DISTINCT sobre p.id evita que me traiga productos repetidos si se vendió o compró el producto en varias ocasiones
            DISTINCT(p.id), p.name, p.description, p.price, p.category_id, imo.user_id
        FROM products p
            INNER JOIN inventory_movements imo  ON p.id             = imo.product_id    -- Necesario para saber que usuario compró o vendió el producto
            INNER JOIN inventory_stocks ist     ON p.id             = ist.product_id    -- Necesario para obtener los productos en stock
        WHERE   imo.user_id     = user_id
        AND     ist.quantity    > 0;
END;
/