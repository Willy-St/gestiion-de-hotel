using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservaHotel.Web.Data;
using ReservaHotel.Web.Models;
using ReservaHotel.Web.ViewModels;

namespace ReservaHotel.Web.Controllers;

[Authorize]
public class ReservasController(HotelDbContext db) : Controller
{
    public async Task<IActionResult> Index()
    {
        var reservas = await db.Reservas
            .Include(x => x.Cliente)
            .Include(x => x.Detalles)
            .OrderByDescending(x => x.Id)
            .ToListAsync();

        return View(reservas);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var model = new ReservaCreateViewModel();
        await CargarListas(model);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ReservaCreateViewModel model)
    {
        var dias = (model.FechaSalida.Date - model.FechaLlegada.Date).Days;

        if (model.FechaLlegada.Date < DateTime.Today)
            ModelState.AddModelError(nameof(model.FechaLlegada), "La fecha de llegada no puede ser anterior a hoy.");

        if (dias <= 0)
            ModelState.AddModelError(nameof(model.FechaSalida), "La salida debe ser posterior a la llegada.");

        if (model.HabitacionesSeleccionadas is null || model.HabitacionesSeleccionadas.Count == 0)
            ModelState.AddModelError(nameof(model.HabitacionesSeleccionadas), "Selecciona al menos una habitación.");

        if (!ModelState.IsValid)
        {
            await CargarListas(model);
            return View(model);
        }

        var cliente = await db.Clientes
            .SingleOrDefaultAsync(x => x.Id == model.ClienteId && x.Activo);

        if (cliente is null)
        {
            ModelState.AddModelError(nameof(model.ClienteId), "El cliente no existe o está inactivo.");
            await CargarListas(model);
            return View(model);
        }

        var ids = model.HabitacionesSeleccionadas.Distinct().ToList();
        var habitaciones = await db.Habitaciones
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();

        if (habitaciones.Count != ids.Count || habitaciones.Any(x => x.Estado != EstadoHabitacion.Disponible))
        {
            ModelState.AddModelError("", "Una o más habitaciones ya no están disponibles. Actualiza la pantalla e inténtalo nuevamente.");
            await CargarListas(model);
            return View(model);
        }

        foreach (var habitacion in habitaciones)
        {
            if (!model.Huespedes.TryGetValue(habitacion.Id, out var huesped) ||
                string.IsNullOrWhiteSpace(huesped))
            {
                ModelState.AddModelError("", $"Ingresa el nombre del huésped para la habitación {habitacion.NumeroHabitacion}.");
            }
        }

        if (!ModelState.IsValid)
        {
            await CargarListas(model);
            return View(model);
        }

        var recepcionistaIdText = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(recepcionistaIdText, out var recepcionistaId))
            return Challenge();

        await using var transaction = await db.Database.BeginTransactionAsync();

        var detalles = habitaciones.Select(h => new DetalleReserva
        {
            HabitacionId = h.Id,
            NombreHuesped = model.Huespedes[h.Id].Trim(),
            CostoDia = h.CostoDia,
            CantidadDias = dias,
            MontoPagar = h.CostoDia * dias
        }).ToList();

        var subtotal = detalles.Sum(x => x.MontoPagar);

        var reserva = new Reserva
        {
            FechaLlegada = model.FechaLlegada.Date,
            FechaSalida = model.FechaSalida.Date,
            CantidadDias = dias,
            ClienteId = cliente.Id,
            RecepcionistaId = recepcionistaId,
            Subtotal = subtotal,
            MontoTotal = subtotal,
            FechaRegistro = DateTime.Now,
            Detalles = detalles
        };

        db.Reservas.Add(reserva);

        foreach (var habitacion in habitaciones)
            habitacion.Estado = EstadoHabitacion.Reservado;

        await db.SaveChangesAsync();

        reserva.NumeroReserva = $"RES-{reserva.Id:000000}";
        await db.SaveChangesAsync();
        await transaction.CommitAsync();

        TempData["Exito"] = $"Reserva {reserva.NumeroReserva} generada correctamente.";
        return RedirectToAction(nameof(Details), new { id = reserva.Id });
    }

    public async Task<IActionResult> Details(int id)
    {
        var reserva = await db.Reservas
            .Include(x => x.Cliente)
            .Include(x => x.Recepcionista)
            .Include(x => x.Detalles)
                .ThenInclude(x => x.Habitacion)
            .SingleOrDefaultAsync(x => x.Id == id);

        if (reserva is null) return NotFound();
        return View(reserva);
    }

    private async Task CargarListas(ReservaCreateViewModel model)
    {
        model.Clientes = await db.Clientes
            .Where(x => x.Activo)
            .OrderBy(x => x.Apellidos)
            .ThenBy(x => x.Nombres)
            .ToListAsync();

        model.Habitaciones = await db.Habitaciones
            .Where(x => x.Estado == EstadoHabitacion.Disponible)
            .OrderBy(x => x.NumeroHabitacion)
            .ToListAsync();
    }
}
