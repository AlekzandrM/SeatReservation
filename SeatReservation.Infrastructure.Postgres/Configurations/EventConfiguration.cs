using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Domain.Events;
using SeatReservation.Domain.Venues;
using SeatReservation.Infrastructure.Postgres.Converters;

namespace SeatReservation.Infrastructure.Postgres.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("events");

        builder.HasKey(e => e.Id).HasName("pk_events");
        builder.Property(e => e.Id)
            .HasConversion(e => e.Value, id => new EventId(id)).HasColumnName("id");

        // 1. The relationship between Tables without navigation property.
        // Event and Venue is one-to-many. Provides consistency with domain models.
        builder.HasOne<Venue>()
            .WithMany()
            .HasForeignKey(e => e.VenueId)
            .IsRequired().OnDelete(DeleteBehavior.Cascade);

        builder.Property(e => e.VenueId).HasColumnName("venue_id");

        // Convert enum to string
        builder.Property(e => e.Type).HasConversion<string>().HasColumnName("type");
        builder.Property(e => e.Info).HasConversion(new EventInfoConverter());
    }
}