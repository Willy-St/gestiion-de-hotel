using System.ComponentModel.DataAnnotations;

namespace ReservaHotel.Web.Models;

public class Recepcionista
{
    public int Id { get; set; }

    [Required, StringLength(120)]
    public string Nombre { get; set; } = "";

    [Required, StringLength(50)]
    public string Usuario { get; set; } = "";

    [Required]
    public string PasswordHash { get; set; } = "";
}
