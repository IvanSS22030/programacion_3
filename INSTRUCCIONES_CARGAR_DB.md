# Cómo Cargar la Base de Datos en pgAdmin

Como no tengo acceso directo a tu instalación de PostgreSQL, debes ejecutar el script manualmente. Es muy fácil:

1.  **Abrir pgAdmin**: Ya lo tienes abierto (según tu imagen).
2.  **Abrir Query Tool**:
    *   Haz clic derecho sobre tu servidor (ej. "PostgreSQL 17" o "PostgreSQL 16").
    *   Selecciona **Query Tool**.
3.  **Crear la Base de Datos**:
    *   Copia y pega este código en el editor y presiona el botón "Play" (Ejecutar):
        ```sql
        CREATE DATABASE "PacientesDB";
        ```
    *   Una vez creada, cierra esa pestaña del Query Tool.
4.  **Refrescar**:
    *   Haz clic derecho en "Databases" y selecciona **Refresh**.
    *   Ahora deberías ver "PacientesDB".
5.  **Crear Tablas y Funciones**:
    *   Haz clic derecho sobre la nueva base de datos **"PacientesDB"**.
    *   Selecciona **Query Tool** (asegúrate de que sea sobre PacientesDB esta vez).
    *   Copia y pega todo el contenido del archivo `Scripts/create_pacientes_db.sql`.
    *   Presiona "Play" (Ejecutar).
6.  **Verificar**:
    *   Expande Schemas -> public -> Tables.
    *   Deberías ver la tabla `pacientes`.
