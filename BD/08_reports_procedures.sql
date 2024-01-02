-- PRODUCTS GET ALL BY SELLER
CREATE OR REPLACE PROCEDURE proc_get_all_products_by_seller(
    p_user_id   IN OUT inventory_movements.user_id%TYPE,
    v_products  OUT SYS_REFCURSOR
)
AS
BEGIN
    OPEN v_products FOR
        SELECT -- El DISTINCT sobre p.id evita que me traiga productos repetidos si se vendió o compró el producto en varias ocasiones
            DISTINCT(p.id), p.name, p.description, p.price, moves.quantity
        FROM products p
            INNER JOIN inventory_movements  moves   ON p.id = moves.product_id    -- Necesario para saber que usuario compró o vendió el producto
            INNER JOIN inventory_stocks     stock   ON p.id = stock.product_id    -- Necesario para obtener los productos en stock
        WHERE   moves.user_id   = p_user_id
        AND     stock.quantity  > 0;
END;
/