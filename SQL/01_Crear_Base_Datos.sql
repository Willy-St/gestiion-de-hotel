IF DB_ID(N'ReservaHotelDB') IS NULL
BEGIN
    CREATE DATABASE ReservaHotelDB;
    PRINT 'Base de datos ReservaHotelDB creada correctamente.';
END
ELSE
BEGIN
    PRINT 'La base de datos ReservaHotelDB ya existe.';
END
GO
