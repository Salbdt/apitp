-- USERS CREATE
CREATE OR REPLACE PROCEDURE proc_users_create(
    new_role_id     IN  users.role_id%TYPE,
    new_name        IN  users.name%TYPE,
    new_email       IN  users.email%TYPE,
    new_password    IN  users.password%TYPE,
    v_user          OUT SYS_REFCURSOR
)
AS
v_user_id   users.id%TYPE;
BEGIN
    INSERT INTO users (id, role_id, name, email, password, created_at)
    VALUES (0, new_role_id, new_name, LOWER(new_email), STANDARD_HASH(new_password, 'SHA512'), SYSDATE)
    RETURNING id into v_user_id;
    
    OPEN v_user FOR
        SELECT u.id, u.role_id, r.name as role_name, r.description as role_description, u.name, u.email
        FROM users u INNER JOIN roles r ON u.role_id = r.id
        WHERE u.id = v_user_id;
    
    COMMIT;
END;
/

-- USERS GET ALL
CREATE OR REPLACE PROCEDURE proc_users_get_all(
    v_users     OUT SYS_REFCURSOR
)
AS
BEGIN
    OPEN v_users FOR
        SELECT u.id, u.role_id, r.name as role_name, r.description as role_description, u.name, u.email
        FROM users u INNER JOIN roles r ON u.role_id = r.id;
END;
/

-- USERS GET BY ID
CREATE OR REPLACE PROCEDURE proc_users_get_by_id(
    user_id     IN users.id%TYPE,
    v_user     OUT SYS_REFCURSOR
)
AS
BEGIN
    OPEN v_user FOR
        SELECT u.id, u.role_id, r.name as role_name, r.description as role_description, u.name, u.email
        FROM users u INNER JOIN roles r ON u.role_id = r.id
        WHERE u.id = user_id;
END;
/

-- USERS GET BY ID
CREATE OR REPLACE PROCEDURE proc_users_get_by_email(
    p_user_email    IN users.email%TYPE,
    v_user          OUT SYS_REFCURSOR
)
AS
BEGIN
    OPEN v_user FOR
        SELECT u.id, u.role_id, r.name as role_name, r.description as role_description, u.name, u.email
        FROM users u INNER JOIN roles r ON u.role_id = r.id
        WHERE u.email = p_user_email;
END;
/

-- USERS LOGIN
CREATE OR REPLACE PROCEDURE proc_users_login(
    p_user_email    IN users.email%TYPE,
    p_user_password IN users.password%TYPE,
    v_user          OUT SYS_REFCURSOR
)
AS
BEGIN
    OPEN v_user FOR
        SELECT u.id, u.role_id, r.name as role_name, r.description as role_description, u.name, u.email
        FROM users u INNER JOIN roles r ON u.role_id = r.id
        WHERE   u.email     = LOWER(p_user_email)
        AND     u.password  = STANDARD_HASH(p_user_password, 'SHA512');
END;
/

-- USERS UPDATE
CREATE OR REPLACE PROCEDURE proc_users_update(
    user_id         in users.id%TYPE,
    user_email      in users.email%TYPE,
    user_password   in users.password%TYPE,
    new_role_id     in users.role_id%TYPE,
    new_name        in users.name%TYPE,
    new_email       in users.email%TYPE,
    new_password    in users.password%TYPE,
    v_result        OUT NUMBER
)
AS
BEGIN
    UPDATE users SET
        role_id     = new_role_id,
        name        = new_name,
        email       = LOWER(new_email),
        password    = STANDARD_HASH(new_password, 'SHA512'),
        updated_at  = SYSDATE
    WHERE id        = user_id
    AND email       = LOWER(user_email)
    AND password    = STANDARD_HASH(user_password, 'SHA512');

    v_result := SQL%ROWCOUNT;
        
    COMMIT;
END;
/

-- USERS DELETE
CREATE OR REPLACE PROCEDURE proc_users_delete(
    user_id     IN users.id%TYPE,
    v_result    OUT NUMBER
)
AS
BEGIN
    DELETE FROM users
    WHERE id = user_id;
    
    v_result := SQL%ROWCOUNT;
    
    COMMIT;
END;
/