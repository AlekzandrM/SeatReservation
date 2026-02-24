using Microsoft.EntityFrameworkCore;
using SeatReservation.Domain.Users;
using SeatReservation.Domain.Venues;

namespace SeatReservation.Infrastructure.Postgres;

public class ReservationServiceDbContext(string connectionString) : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReservationServiceDbContext).Assembly);
    }

    public DbSet<Venue> Venues => Set<Venue>();
    public DbSet<User> Users => Set<User>();
}
