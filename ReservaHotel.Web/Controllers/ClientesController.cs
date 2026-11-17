using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservaHotel.Web.Data;
using ReservaHotel.Web.Models;

namespace ReservaHotel.Web.Controllers;

[Authorize]
public class ClientesController(HotelDbContext db) : Controller
{
    public async Task<IActionResult> Index()
    {
        var clientes = await db.Clientes.OrderBy(x => x.Apellidos).ThenBy(x => x.Nombres).ToListAsync();
        return View(clientes);
    }

    [HttpGet]
    public IActionResult Create() => View(new Cliente());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Cliente model)
    {
        if (await db.Clientes.AnyAsync(x => x.Codigo == model.Codigo))
            ModelState.AddModelError(nameof(model.Codigo), "Ese código de cliente ya existe.");

        if (!ModelState.IsValid) return View(model);

        model.Codigo = model.Codigo.Trim();
        model.Nombres = model.Nombres.Trim();
        model.Apellidos = model.Apellidos.Trim();
        db.Clientes.Add(model);
        await db.SaveChangesAsync();

        TempData["Exito"] = "Cliente registrado correctamente.";
        return RedirectToAction(nameof(Index));
    }
}
