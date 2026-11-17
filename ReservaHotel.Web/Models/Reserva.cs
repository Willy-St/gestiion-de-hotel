using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservaHotel.Web.Models;

public class Reserva
{
    public int Id { get; set; }

    [StringLength(30)]
    public string? NumeroReserva { get; set; }

    public DateTime FechaLlegada { get; set; }
    public DateTime FechaSalida { get; set; }
    public int CantidadDias { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Subtotal { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal MontoTotal { get; set; }

    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; }

    public int RecepcionistaId { get; set; }
    public Recepcionista? Recepcionista { get; set; }

    public ICollection<DetalleReserva> Detalles { get; set; } = new List<DetalleReserva>();
}
