-- Script for Inventory Table
-- \c PacientesDB

CREATE TABLE IF NOT EXISTS Inventario (
    id SERIAL PRIMARY KEY,
    code VARCHAR(50) NOT NULL,
    name VARCHAR(100) NOT NULL,
    type VARCHAR(20) NOT NULL,
    quantity INT NOT NULL,
    date TIMESTAMP NOT NULL
);

-- CRUD Procedures

CREATE OR REPLACE PROCEDURE sp_insert_inventory(
    p_code VARCHAR,
    p_name VARCHAR,
    p_type VARCHAR,
    p_quantity INT,
    p_date TIMESTAMP
)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO Inventario (code, name, type, quantity, date)
    VALUES (p_code, p_name, p_type, p_quantity, p_date);
END;
$$;

CREATE OR REPLACE PROCEDURE sp_delete_inventory(p_id INT)
LANGUAGE plpgsql
AS $$
BEGIN
    DELETE FROM Inventario WHERE id = p_id;
END;
$$;

-- Actualización de repositorio - 2026-04-08

-- Refine database query execution
