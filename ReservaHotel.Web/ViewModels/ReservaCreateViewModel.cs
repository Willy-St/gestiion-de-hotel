using System.ComponentModel.DataAnnotations;
using ReservaHotel.Web.Models;

namespace ReservaHotel.Web.ViewModels;

public class ReservaCreateViewModel
{
    [Required(ErrorMessage = "Selecciona un cliente.")]
    [Display(Name = "Cliente")]
    public int? ClienteId { get; set; }

    [Required, DataType(DataType.Date)]
    [Display(Name = "Fecha de llegada")]
    public DateTime FechaLlegada { get; set; } = DateTime.Today;

    [Required, DataType(DataType.Date)]
    [Display(Name = "Fecha de salida")]
    public DateTime FechaSalida { get; set; } = DateTime.Today.AddDays(1);

    public List<int> HabitacionesSeleccionadas { get; set; } = new();
    public Dictionary<int, string> Huespedes { get; set; } = new();

    // Datos auxiliares para mostrar el formulario; no se guardan como columnas.
    public List<Cliente> Clientes { get; set; } = new();
    public List<Habitacion> Habitaciones { get; set; } = new();
}
