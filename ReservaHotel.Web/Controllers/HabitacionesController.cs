using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservaHotel.Web.Data;
using ReservaHotel.Web.Models;

namespace ReservaHotel.Web.Controllers;

[Authorize]
public class HabitacionesController(HotelDbContext db) : Controller
{
    public async Task<IActionResult> Index()
    {
        var habitaciones = await db.Habitaciones
            .OrderBy(x => x.NumeroHabitacion)
            .ToListAsync();
        return View(habitaciones);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CambiarEstado(int id, string estado)
    {
        if (!EstadoHabitacion.Todos.Contains(estado))
        {
            TempData["Error"] = "El estado indicado no es válido.";
            return RedirectToAction(nameof(Index));
        }

        var habitacion = await db.Habitaciones.FindAsync(id);
        if (habitacion is null) return NotFound();

        habitacion.Estado = estado;
        await db.SaveChangesAsync();

        TempData["Exito"] = $"La habitación {habitacion.NumeroHabitacion} ahora está {estado}.";
        return RedirectToAction(nameof(Index));
    }
}
