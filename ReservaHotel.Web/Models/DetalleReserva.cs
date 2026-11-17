using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservaHotel.Web.Models;

public class DetalleReserva
{
    public int Id { get; set; }

    public int ReservaId { get; set; }
    public Reserva? Reserva { get; set; }

    public int HabitacionId { get; set; }
    public Habitacion? Habitacion { get; set; }

    [Required, StringLength(150)]
    public string NombreHuesped { get; set; } = "";

    [Column(TypeName = "decimal(10,2)")]
    public decimal CostoDia { get; set; }

    public int CantidadDias { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal MontoPagar { get; set; }
}
