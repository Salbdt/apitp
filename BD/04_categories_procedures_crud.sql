-- CATEGORY CREATE
CREATE OR REPLACE PROCEDURE proc_categories_create(
    new_name        IN categories.name%TYPE,
    new_description IN categories.description%TYPE,
    v_category      OUT SYS_REFCURSOR
)
AS
v_category_id   categories.id%TYPE;
BEGIN
    INSERT INTO categories
        (id, name, description, created_at)
    VALUES
        (0, new_name, new_description, SYSDATE)
    RETURNING id into v_category_id;
        
    OPEN v_category FOR
        SELECT
            id, name, description
        FROM categories
        WHERE
            id = v_category_id;
            
    COMMIT;
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
    new_description     IN categories.description%TYPE,
    v_category          OUT SYS_REFCURSOR
)
AS
v_updated_at        categories.updated_at%TYPE;
BEGIN
    UPDATE categories
    SET
        name        = new_name,
        description = new_description,
        updated_at  = SYSDATE
    WHERE
        id          = category_id
    RETURNING updated_at into v_updated_at;
        
    OPEN v_category FOR
        SELECT
            id
        FROM categories
        WHERE
            id          = category_id
        AND
            updated_at  = v_updated_at;
    
    COMMIT;
END;
/

-- CATEGORY DELETE
CREATE OR REPLACE PROCEDURE proc_categories_delete(
    category_id IN categories.id%TYPE,
    v_category          OUT SYS_REFCURSOR
)
AS
BEGIN        
    OPEN v_category FOR
        SELECT
            id
        FROM categories
        WHERE
            id = category_id;
            
    DELETE FROM categories
    WHERE
        id = category_id;
        
    COMMIT;
END;
/