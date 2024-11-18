CREATE TABLE productos (
    id SERIAL PRIMARY KEY,
    nombre VARCHAR(255),
    cantidad INT,
    precio FLOAT
);

INSERT INTO productos (nombre, cantidad, precio)
VALUES
    ('Arroz', 10, 20.5),
    ('Papas', 5, 15.75),
    ('Sopa', 3, 50.99),
    ('Atún', 5, 11.99),
    ('Fideos', 7, 13.99);