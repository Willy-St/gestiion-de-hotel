using Microsoft.EntityFrameworkCore;
using ReservaHotel.Web.Models;

namespace ReservaHotel.Web.Data;

public class HotelDbContext(DbContextOptions<HotelDbContext> options) : DbContext(options)
{
    public DbSet<Recepcionista> Recepcionistas => Set<Recepcionista>();
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Habitacion> Habitaciones => Set<Habitacion>();
    public DbSet<Reserva> Reservas => Set<Reserva>();
    public DbSet<DetalleReserva> DetallesReserva => Set<DetalleReserva>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Recepcionista>().HasIndex(x => x.Usuario).IsUnique();
        modelBuilder.Entity<Cliente>().HasIndex(x => x.Codigo).IsUnique();
        modelBuilder.Entity<Habitacion>().HasIndex(x => x.NumeroHabitacion).IsUnique();
        modelBuilder.Entity<Reserva>().HasIndex(x => x.NumeroReserva).IsUnique()
            .HasFilter("[NumeroReserva] IS NOT NULL");

        modelBuilder.Entity<Reserva>()
            .HasOne(x => x.Cliente)
            .WithMany()
            .HasForeignKey(x => x.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Reserva>()
            .HasOne(x => x.Recepcionista)
            .WithMany()
            .HasForeignKey(x => x.RecepcionistaId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Reserva>()
            .HasMany(x => x.Detalles)
            .WithOne(x => x.Reserva)
            .HasForeignKey(x => x.ReservaId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<DetalleReserva>()
            .HasOne(x => x.Habitacion)
            .WithMany()
            .HasForeignKey(x => x.HabitacionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
