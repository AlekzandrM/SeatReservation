using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Domain.Venues;
using Shared.Constants;

namespace SeatReservation.Infrastructure.Postgres.Configurations;

public class VenueConfiguration : IEntityTypeConfiguration<Venue>
{
    public void Configure(EntityTypeBuilder<Venue> builder)
    {
        builder.ToTable("venues");

        // 1. Constraint for primary key
        builder.HasKey(v => v.Id).HasName("pk_venues");
        builder.Property(v => v.Id)
            .HasConversion(v => v.Value, id => new VenueId(id)).HasColumnName("id");

        // 2builder.ComplexProperty does not support nullable types VenueName? Name.
        // So we use builder.OwnsOne + builder.Navigation
        builder.OwnsOne(v => v.Name, nameBuilder =>
        {
            nameBuilder.Property(n => n.Prefix).HasMaxLength(LengthConstants.Length50).HasColumnName("prefix");
            nameBuilder.Property(n => n.Name).HasMaxLength(LengthConstants.Length500).HasColumnName("name");
        });
        builder.Navigation(v => v.Name).IsRequired(false);

        // 3. The relationship between Tables
        // Venue and Seat is one-to-many (one venue has many seats)
        // builder.HasMany(v => v.Seats) or
        builder.HasMany<Seat>()
            .WithOne()
            .HasForeignKey(s => s.VenueId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}