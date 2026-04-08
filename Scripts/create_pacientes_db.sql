-- 6. Creaci贸n de Base de Datos
-- Base de Datos: PostgreSQL
-- Nombre: PacientesDB

-- NOTA: Ejecutar este script en una herramienta como pgAdmin o psql.

-- 1. Creaci贸n de la Base de Datos
-- CREATE DATABASE "PacientesDB";

-- Conectarse a la base de datos antes de ejecutar lo siguiente:
-- \c PacientesDB

-- 2. Creaci贸n de la Tabla Pacientes
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
-- Funci贸n para obtener todos los pacientes
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

-- Delete 
CREATE OR REPLACE PROCEDURE sp_delete_paciente(p_codigo INT)
LANGUAGE plpgsql
AS $$
BEGIN
    DELETE FROM Pacientes WHERE codigopac = p_codigo;
END;
$$;


-- Actualizaci髇 de repositorio - 2026-04-08

-- Refine database query execution
