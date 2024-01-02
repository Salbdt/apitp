# APITP
Servicio API con ASP.NET Core. Utilizand C# y Oracle.

## Características
 - La conexión se realiza conectando la base de datos directo (sin usar EF o Dapper).
 - Se utilizan Procedimientos Oracle para realizar un CRUD y algunos Endpoints adicionales.
 - Se utiliza JWT Authorization para obtener un token al loguearse y poder consultar otros Endpoints.

### Base de datos
 - La base de datos cuenta con las siguientes tablas:
    - roles y users
    - categories y products
    - inventory_stocks e inventory_movements

### Endpoints
 - Roles
    - GetAll
    - GetById
 - Users
    - Register
    - GetAll
    - GetById
    - Login
    - Update
    - Delete
 - Categories y Products
    - Create
    - GetAll
    - GetById
    - Update
    - Delete
 - Inventory Stocks
    - GetAll
    - GetById
    - Update
 - Inventory Movements
    - Create Purchase
    - GetAll
    - GetPurchaseResume
    - Update
    - Delete
 - Reports
    - GetAllProductsBySeller

## Requisitos
 - Oracle XE o Instant Client
 - SQL Developer u otro IDE que pueda utilizar Oracle
 - .NET 8
 - Postman o similar que permita el ingreso de token

## Instalación del proyecto
 - Descargar o clonar el repositorio.
 - Utilizar los scripts de la carpeta "BD" numerados, ejecutándolos en orden.
    - En este caso, el script "00_inventory_user.sql" crea un usuario nuevo con una contraseña que pueden cambiar.
    - Hay que crear una nueva conexión con SQL Developer
    - Todos los scripts restantes se tienen que conectar a esta conexion.
 - Abrir una consola, posicionarse en "Inventory.WebAPI" y ejecutar el servicio con "dotnet run".
    - Si no hay errores apareceran unas líneas con info, entre ellas la ruta con el puerto para acceder.
 - Abrir Postman e importar el archivo "APITP.postman_collection.json".
    - Tiene 2 carpetas.
        - "General" con todos los Endpoints bien separados.
        - "Endpoints Obligatorios" con los pedidos en el TP.
 - Antes de hacer las pruebas, en la carpeta "BD" se encuentra el script "inserts_bd.sql" que tiene unos pocos datos para ayudar en las pruebas.
 - Finalmente si el servicio está ejecutándose, se puede probar los distintos Endpoints.
