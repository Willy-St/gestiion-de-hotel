/*
  Base de datos del Sistema de Reserva de Habitaciones.
  Ejecuta este script en SQL Server Management Studio.
  Al iniciar la aplicación por primera vez, Entity Framework Core crea las tablas
  y carga datos de demostración dentro de esta base de datos.
*/
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
