using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservaHotel.Web.Models;

public class Cliente
{
    public int Id { get; set; }

    [Required, StringLength(20)]
    public string Codigo { get; set; } = "";

    [Required, StringLength(100)]
    public string Nombres { get; set; } = "";

    [Required, StringLength(100)]
    public string Apellidos { get; set; } = "";

    public bool Activo { get; set; } = true;

    [NotMapped]
    public string NombreCompleto => $"{Nombres} {Apellidos}";
}
