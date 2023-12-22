-- ROLES GET ALL
CREATE OR REPLACE PROCEDURE proc_roles_get_all(
    v_roles     OUT SYS_REFCURSOR
)
AS
BEGIN
    OPEN v_roles FOR
        SELECT
            id, name, description
        FROM roles;
END;
/

-- ROLES GET BY ID
create or replace procedure proc_roles_get_by_id(
    role_id     IN roles.id%TYPE,
    v_role      OUT SYS_REFCURSOR
)
as
begin
    OPEN v_role FOR
        SELECT
            id, name, description
        FROM roles
        WHERE
            id = role_id;
end;
/