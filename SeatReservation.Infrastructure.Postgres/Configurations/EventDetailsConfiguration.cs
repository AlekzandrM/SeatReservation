using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Domain.Events;

namespace SeatReservation.Infrastructure.Postgres.Configurations;

public class EventDetailsConfiguration : IEntityTypeConfiguration<EventDetails>
{
    public void Configure(EntityTypeBuilder<EventDetails> builder)
    {
        builder.ToTable("events_details");

        // Constraint Primary Key = Foreign Key
        builder.HasKey(ed => ed.EventId).HasName("pk_event_details");
        builder.Property(ed => ed.EventId)
            .HasConversion(ed => ed.Value, id => new EventId(id))
            .ValueGeneratedNever()
            .HasColumnName("event_id");

        // Event and Details is one-to-one
        builder.HasOne<Event>()
            .WithOne(e => e.Details)
            .HasForeignKey<EventDetails>(ed => ed.EventId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(ed => ed.Capacity);
        builder.Property(ed => ed.Description);
    }
}