-- Datos de prueba

DECLARE
    v_result SYS_REFCURSOR;

BEGIN
    -- INSERT USERS
    proc_users_create(1, 'Admin', 'admin@mail.com', '123', v_result);
    
    proc_users_create(2, 'Vendedor 1', 'vendedor1@mail.com', '123', v_result);
    proc_users_create(2, 'Vendedor 2', 'vendedor2@mail.com', '123', v_result);
    proc_users_create(2, 'Vendedor 3', 'vendedor3@mail.com', '123', v_result);
    
    proc_users_create(3, 'Comprador 1', 'comprador1@mail.com', '123', v_result);
    proc_users_create(3, 'Comprador 2', 'comprador2@mail.com', '123', v_result);
    proc_users_create(3, 'Comprador 3', 'comprador3@mail.com', '123', v_result);
    
    -- INSERT CATEGORIES
    proc_categories_create('Procesador', 'Componente de computadora', v_result);
    proc_categories_create('Placa madre', 'Componente de computadora', v_result);
    proc_categories_create('Memoria RAM', 'Componente de computadora', v_result);
    
    -- INSERT PRODUCTS
    proc_products_create(1, 'Procesador Básico', 'Procesador de computadora barato', 50, v_result);
    proc_products_create(1, 'Procesador Medio', 'Procesador de computadora', 90, v_result);
    proc_products_create(1, 'Procesador Bueno', 'Procesador de computadora caro', 150, v_result);
    
    proc_products_create(2, 'Placa Básica', 'Placa madre de computadora barata', 60, v_result);
    proc_products_create(2, 'Placa Media', 'Placa madre de computadora', 75, v_result);
    proc_products_create(2, 'Placa Buena', 'Placa madre de computadora cara', 125, v_result);
    
    proc_products_create(3, 'Memoria 2 GB', 'Memoria de computadora', 10, v_result);
    proc_products_create(3, 'Memoria 4 GB', 'Memoria de computadora', 20, v_result);
    proc_products_create(3, 'Memoria 8 GB', 'Memoria de computadora', 40, v_result);
    
    -- INSERT MOVEMENTS
    proc_inventory_movements_create(1, 5, 50, 'COMPRA', v_result);
    proc_inventory_movements_create(2, 5, 40, 'COMPRA', v_result);
    proc_inventory_movements_create(3, 5, 10, 'COMPRA', v_result);
    
    proc_inventory_movements_create(4, 6, 50, 'COMPRA', v_result);
    proc_inventory_movements_create(5, 6, 50, 'COMPRA', v_result);
    proc_inventory_movements_create(6, 6, 15, 'COMPRA', v_result);
    
    proc_inventory_movements_create(7, 7, 100, 'COMPRA', v_result);
    proc_inventory_movements_create(8, 7, 100, 'COMPRA', v_result);
    proc_inventory_movements_create(9, 7, 50, 'COMPRA', v_result);
    
    proc_inventory_movements_create(1, 2, 1, 'VENTA', v_result);
    proc_inventory_movements_create(4, 2, 1, 'VENTA', v_result);
    proc_inventory_movements_create(7, 2, 2, 'VENTA', v_result);
    
    proc_inventory_movements_create(1, 3, 1, 'VENTA', v_result);
    proc_inventory_movements_create(5, 3, 1, 'VENTA', v_result);
    proc_inventory_movements_create(7, 3, 4, 'VENTA', v_result);
    
    proc_inventory_movements_create(2, 4, 1, 'VENTA', v_result);
    proc_inventory_movements_create(5, 4, 1, 'VENTA', v_result);
    proc_inventory_movements_create(9, 4, 1, 'VENTA', v_result);
END;
    