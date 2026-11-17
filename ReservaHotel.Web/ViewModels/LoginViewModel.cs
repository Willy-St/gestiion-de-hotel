using System.ComponentModel.DataAnnotations;

namespace ReservaHotel.Web.ViewModels;

public class LoginViewModel
{
    [Required, StringLength(50)]
    public string Usuario { get; set; } = "";

    [Required, DataType(DataType.Password)]
    public string Contrasena { get; set; } = "";
}
