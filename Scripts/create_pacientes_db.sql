-- 6. Creación de Base de Datos
-- Base de Datos: PostgreSQL
-- Nombre: PacientesDB

-- NOTA: Ejecutar este script en una herramienta como pgAdmin o psql.

-- 1. Creación de la Base de Datos
-- CREATE DATABASE "PacientesDB";

-- Conectarse a la base de datos antes de ejecutar lo siguiente:
-- \c PacientesDB

-- 2. Creación de la Tabla Pacientes
CREATE TABLE IF NOT EXISTS Pacientes (
    codigopac SERIAL PRIMARY KEY,
    nombrepac VARCHAR(100) NOT NULL,
    apelldiopac VARCHAR(100) NOT NULL,
    direccionpac VARCHAR(255),
    telefonopac VARCHAR(20),
    estatuspac VARCHAR(20) DEFAULT 'Activo'
);

-- 3. CRUD para la tabla Pacientes

-- Create (Insertar)
CREATE OR REPLACE PROCEDURE sp_insert_paciente(
    p_nombre VARCHAR,
    p_apellido VARCHAR,
    p_direccion VARCHAR,
    p_telefono VARCHAR,
    p_estatus VARCHAR
)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO Pacientes (nombrepac, apelldiopac, direccionpac, telefonopac, estatuspac)
    VALUES (p_nombre, p_apellido, p_direccion, p_telefono, p_estatus);
END;
$$;

-- Read (Leer/Seleccionar)
-- Función para obtener todos los pacientes
CREATE OR REPLACE FUNCTION fn_get_all_pacientes()
RETURNS TABLE (
    codigopac INT,
    nombrepac VARCHAR,
    apelldiopac VARCHAR,
    direccionpac VARCHAR,
    telefonopac VARCHAR,
    estatuspac VARCHAR
)
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY SELECT * FROM Pacientes;
END;
$$;

-- Update (Actualizar)
CREATE OR REPLACE PROCEDURE sp_update_paciente(
    p_codigo INT,
    p_nombre VARCHAR,
    p_apellido VARCHAR,
    p_direccion VARCHAR,
    p_telefono VARCHAR,
    p_estatus VARCHAR
)
LANGUAGE plpgsql
AS $$
BEGIN
    UPDATE Pacientes
    SET nombrepac = p_nombre,
        apelldiopac = p_apellido,
        direccionpac = p_direccion,
        telefonopac = p_telefono,
        estatuspac = p_estatus
    WHERE codigopac = p_codigo;
END;
$$;

-- Delete (Eliminar)
CREATE OR REPLACE PROCEDURE sp_delete_paciente(p_codigo INT)
LANGUAGE plpgsql
AS $$
BEGIN
    DELETE FROM Pacientes WHERE codigopac = p_codigo;
END;
$$;

-- Ejemplos de uso:
-- CALL sp_insert_paciente('Juan', 'Perez', 'Calle 123', '555-1234', 'Activo');
-- SELECT * FROM fn_get_all_pacientes();
-- CALL sp_update_paciente(1, 'Juan Updated', 'Perez', 'Calle 123', '555-5555', 'Inactivo');
-- CALL sp_delete_paciente(1);
