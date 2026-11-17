using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservaHotel.Web.Models;

public class Habitacion
{
    public int Id { get; set; }

    [Required, StringLength(10)]
    public string NumeroHabitacion { get; set; } = "";

    [Required, StringLength(50)]
    public string Tipo { get; set; } = "";

    [Column(TypeName = "decimal(10,2)")]
    [Range(0.01, 999999)]
    public decimal CostoDia { get; set; }

    [Required, StringLength(20)]
    public string Estado { get; set; } = EstadoHabitacion.Disponible;
}
