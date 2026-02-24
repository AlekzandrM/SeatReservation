using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Domain.Events;
using SeatReservation.Domain.Reservations;

namespace SeatReservation.Infrastructure.Postgres.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable("reservations");

        builder.HasKey(r => r.Id).HasName("pk_reservations");
        builder.Property(r => r.Id)
            .HasConversion(r => r.Value, id => new ReservationId(id)).HasColumnName("id");


        // Reservation has many ReservedSeats. To have many-to-many relationship we have to configure ReservationSeat too.
        builder.HasMany(r => r.ReservedSeats)
            .WithOne(rs => rs.Reservation)
            .HasForeignKey("reservation_id")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(r => r.EventId)
            .HasConversion(eid => eid.Value, id => new EventId(id));

        builder.Property(r => r.UserId);
        builder.Property(r => r.Status);
        builder.Property(r => r.CreatedAt);
    }
}