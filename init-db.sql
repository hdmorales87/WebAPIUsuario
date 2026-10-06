USE master;
GO

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'db_usuario')
BEGIN
    CREATE DATABASE db_usuario;
END
GO
