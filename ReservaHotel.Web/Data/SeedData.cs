using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReservaHotel.Web.Models;

namespace ReservaHotel.Web.Data;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        var db = services.GetRequiredService<HotelDbContext>();
        var hasher = services.GetRequiredService<IPasswordHasher<Recepcionista>>();

        if (!await db.Recepcionistas.AnyAsync())
        {
            var recepcionista = new Recepcionista
            {
                Nombre = "Recepcionista de prueba",
                Usuario = "recepcionista"
            };
            recepcionista.PasswordHash = hasher.HashPassword(recepcionista, "Hotel123*");
            db.Recepcionistas.Add(recepcionista);
        }

        if (!await db.Clientes.AnyAsync())
        {
            db.Clientes.AddRange(
                new Cliente { Codigo = "CLI-001", Nombres = "Ana", Apellidos = "Torres" },
                new Cliente { Codigo = "CLI-002", Nombres = "Luis", Apellidos = "Mendoza" },
                new Cliente { Codigo = "CLI-003", Nombres = "María", Apellidos = "Rojas" }
            );
        }

        if (!await db.Habitaciones.AnyAsync())
        {
            db.Habitaciones.AddRange(
                new Habitacion { NumeroHabitacion = "101", Tipo = "Simple", CostoDia = 90m, Estado = EstadoHabitacion.Disponible },
                new Habitacion { NumeroHabitacion = "102", Tipo = "Doble", CostoDia = 140m, Estado = EstadoHabitacion.Disponible },
                new Habitacion { NumeroHabitacion = "103", Tipo = "Matrimonial", CostoDia = 160m, Estado = EstadoHabitacion.Disponible },
                new Habitacion { NumeroHabitacion = "104", Tipo = "Suite", CostoDia = 250m, Estado = EstadoHabitacion.Disponible },
                new Habitacion { NumeroHabitacion = "105", Tipo = "Doble", CostoDia = 140m, Estado = EstadoHabitacion.Disponible },
                new Habitacion { NumeroHabitacion = "201", Tipo = "Simple", CostoDia = 95m, Estado = EstadoHabitacion.Disponible }
            );
        }

        await db.SaveChangesAsync();
    }
}
