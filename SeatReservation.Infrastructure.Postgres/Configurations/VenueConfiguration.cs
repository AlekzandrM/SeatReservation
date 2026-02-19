using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Domain.Venues;
using Shared.Constants;

namespace SeatReservation.Infrastructure.Postgres.Configurations;

public class VenueConfiguration : IEntityTypeConfiguration<Venue>
{
    public void Configure(EntityTypeBuilder<Venue> builder)
    {
        builder.ToTable("Venues");
        builder.HasKey(v => v.Id).HasName("id_pk");

        builder.Property(v => v.Name).IsRequired().HasMaxLength(LengthConstants.Length500).HasColumnName("name");
    }
}