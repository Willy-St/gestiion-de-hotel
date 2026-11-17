namespace ReservaHotel.Web.Models;

public static class EstadoHabitacion
{
    public const string Disponible = "Disponible";
    public const string Reservado = "Reservado";
    public const string Ocupado = "Ocupado";

    public static readonly string[] Todos = [Disponible, Reservado, Ocupado];
}
