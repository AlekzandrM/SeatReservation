using Microsoft.EntityFrameworkCore;
using SeatReservation.Domain.Venues;

namespace SeatReservation.Infrastructure.Postgres;

public class ReservationServiceDbContext(string connectionString) : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Venue> Venues => Set<Venue>();
}
