-- CATEGORY CREATE
CREATE OR REPLACE PROCEDURE proc_categories_create(
    new_name        IN categories.name%TYPE,
    new_description IN categories.description%TYPE
)
AS
BEGIN
    INSERT INTO categories
        (id, name, description, created_at)
    VALUES
        (0, new_name, new_description, SYSDATE);
END;
/

-- CATEGORY GET ALL
CREATE OR REPLACE PROCEDURE proc_categories_get_all(
    v_categories    OUT SYS_REFCURSOR
)
AS
BEGIN
    OPEN v_categories FOR
        SELECT
            id, name, description
        FROM categories;
END;
/

-- CATEGORY GET BY ID
CREATE OR REPLACE PROCEDURE proc_categories_get_by_id(
    category_id     IN categories.id%TYPE,
    v_categories    OUT SYS_REFCURSOR
)
AS
BEGIN
    OPEN v_categories FOR
        SELECT
            id, name, description
        FROM categories
        WHERE
            id = category_id;
END;
/

-- CATEGORY UPDATE
CREATE OR REPLACE PROCEDURE proc_categories_update(
    category_id         IN categories.id%TYPE,
    new_name            IN categories.name%TYPE,
    new_description     IN categories.description%TYPE   
)
AS
BEGIN
    UPDATE categories
    SET
        name        = new_name,
        description = new_description,
        updated_at  = SYSDATE
    WHERE
        id          = category_id;
END;
/

-- CATEGORY DELETE
CREATE OR REPLACE PROCEDURE proc_categories_delete(
    category_id IN categories.id%TYPE
)
AS
BEGIN
    DELETE FROM categories
    WHERE
        id = category_id;
END;
/