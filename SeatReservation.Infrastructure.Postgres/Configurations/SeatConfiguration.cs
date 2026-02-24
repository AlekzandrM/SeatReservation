using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Domain.Venues;

namespace SeatReservation.Infrastructure.Postgres.Configurations;

public class SeatConfiguration : IEntityTypeConfiguration<Seat>
{
    public void Configure(EntityTypeBuilder<Seat> builder)
    {
        builder.ToTable("seats");

        builder.HasKey(s => s.Id).HasName("pk_seats");
        builder.Property(s => s.Id)
            .HasConversion(s => s.Value, id => new SeatId(id))
            .HasColumnName("id");

        builder.Property(s => s.VenueId).HasColumnName("venue_id");

        builder.Property(s => s.RowNumber);
        builder.Property(s => s.SeatNumber);
    }
}