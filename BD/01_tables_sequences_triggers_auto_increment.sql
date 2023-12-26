-- ********** TABLES **********

-- ROLES
CREATE TABLE roles(
	id				NUMBER PRIMARY KEY,
	name			VARCHAR2(30) NOT NULL,
	description		VARCHAR2(50),
	created_at		DATE DEFAULT SYSDATE,
	updated_at		DATE NULL
);

INSERT INTO roles (id, name, description) VALUES (1, 'Admin', 'Administrador de la BD');
INSERT INTO roles (id, name, description) VALUES (2, 'Vendedor', 'Vende los productos del inventario');
INSERT INTO roles (id, name, description) VALUES (3, 'Comprador', 'Compra productos para el inventario');

-- USERS
CREATE TABLE users(
	id				NUMBER PRIMARY KEY,
	role_id			NUMBER REFERENCES roles(id),
	name			VARCHAR2(30) NOT NULL,
	email			VARCHAR2(50) NOT NULL,
	password		VARCHAR2(128) NOT NULL,
	created_at		DATE DEFAULT SYSDATE,
	updated_at		DATE NULL
);

-- CATEGORIES
CREATE TABLE categories(
	id				NUMBER PRIMARY KEY,
	name			VARCHAR2(30) NOT NULL,
	description		VARCHAR2(50),
	created_at		DATE DEFAULT SYSDATE,
	updated_at		DATE NULL
);

-- PRODUCTS
CREATE TABLE products(
	id				NUMBER PRIMARY KEY,
	category_id		NUMBER REFERENCES categories(id),
	name			VARCHAR2(30) NOT NULL,
	description		VARCHAR2(50),
    price           NUMBER(10,2),
	created_at		DATE DEFAULT SYSDATE,
	updated_at		DATE NULL
);

-- INVENTORY STOCKS
CREATE TABLE inventory_stocks(    
	id				NUMBER PRIMARY KEY,
	product_id		NUMBER REFERENCES products(id),
    quantity        NUMBER DEFAULT 0,
	created_at		DATE DEFAULT SYSDATE,
	updated_at		DATE NULL
);

-- INVENTORY MOVEMENTS
CREATE TABLE inventory_movements(    
	id				NUMBER PRIMARY KEY,
	product_id		NUMBER REFERENCES products(id),
    user_id         NUMBER REFERENCES users(id),
    quantity        NUMBER NOT NULL,
    movement_type   VARCHAR2(30) NOT NULL,
	movement_date   DATE DEFAULT SYSDATE
);

-- ********** SEQUENCES **********

-- USERS
CREATE SEQUENCE sequence_user_id
START WITH 1 INCREMENT BY 1;

-- CATEGORIES
CREATE SEQUENCE sequence_category_id
START WITH 1 INCREMENT BY 1;

-- PRODUCTS
CREATE SEQUENCE sequence_product_id
START WITH 1 INCREMENT BY 1;

-- INVENTORY STOCKS
CREATE SEQUENCE sequence_inventory_stock_id
START WITH 1 INCREMENT BY 1;

-- INVENTORY MOVEMENTS
CREATE SEQUENCE sequence_inventory_movement_id
START WITH 1 INCREMENT BY 1;

-- ********** TRIGGERS **********

-- USERS
CREATE OR REPLACE TRIGGER trigger_user_id
    BEFORE INSERT
    ON USERS
    FOR EACH ROW
BEGIN
    SELECT sequence_user_id.NEXTVAL INTO :NEW.id FROM dual;
END;
/

-- CATEGORIES
CREATE OR REPLACE TRIGGER trigger_category_id
    BEFORE INSERT
    ON categories
    FOR EACH ROW
BEGIN
    SELECT sequence_category_id.NEXTVAL INTO :NEW.id FROM dual;
END;
/

-- PRODUCTS
CREATE OR REPLACE TRIGGER trigger_product_id
    BEFORE INSERT
    ON products
    FOR EACH ROW
BEGIN
    SELECT sequence_product_id.NEXTVAL INTO :NEW.id FROM dual;
END;
/

-- INVENTORY STOCKS
CREATE OR REPLACE TRIGGER trigger_inventory_stock_id
    BEFORE INSERT
    ON inventory_stocks
    FOR EACH ROW
BEGIN
    SELECT sequence_inventory_stock_id.NEXTVAL INTO :NEW.id FROM dual;
END;
/

-- INVENTORY MOVEMENTS
CREATE OR REPLACE TRIGGER trigger_inventory_movement_id
    BEFORE INSERT
    ON inventory_movements
    FOR EACH ROW
BEGIN
    SELECT sequence_inventory_movement_id.NEXTVAL INTO :NEW.id FROM dual;
END;
/