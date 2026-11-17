using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservaHotel.Web.Data;
using ReservaHotel.Web.Models;

namespace ReservaHotel.Web.Controllers;

[Authorize]
public class HomeController(HotelDbContext db) : Controller
{
    public async Task<IActionResult> Index()
    {
        ViewBag.Clientes = await db.Clientes.CountAsync(x => x.Activo);
        ViewBag.Habitaciones = await db.Habitaciones.CountAsync();
        ViewBag.Disponibles = await db.Habitaciones.CountAsync(x => x.Estado == EstadoHabitacion.Disponible);
        ViewBag.Reservadas = await db.Habitaciones.CountAsync(x => x.Estado == EstadoHabitacion.Reservado);
        ViewBag.Reservas = await db.Reservas.CountAsync();
        ViewBag.UltimasReservas = await db.Reservas
            .Include(x => x.Cliente)
            .OrderByDescending(x => x.Id)
            .Take(5)
            .ToListAsync();

        return View();
    }

    public IActionResult Error() => View();
}
