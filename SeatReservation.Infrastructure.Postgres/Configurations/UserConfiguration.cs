using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Domain.Events;
using SeatReservation.Domain.Users;
using Shared.Constants;

namespace SeatReservation.Infrastructure.Postgres.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Id).HasName("pk_users");
        builder.Property(u => u.Id).HasColumnName("id");

        // 1. HasColumnType("jsonb") suitable for string
        // builder.Property(u => u.SocialNetworks).HasColumnType("jsonb");
        // 2. OwnsMany + ToJson
        // builder.OwnsMany(u => u.SocialNetworks, socialNetworkBuilder =>
        // {
        //     socialNetworkBuilder.ToJson("socials");
        //
        //     socialNetworkBuilder.Property(sn => sn.Link)
        //         .HasJsonPropertyName("link")
        //         .IsRequired().HasMaxLength(LengthConstants.Length500);
        //     socialNetworkBuilder.Property(sn => sn.Name)
        //         .HasJsonPropertyName("name")
        //         .IsRequired().HasMaxLength(LengthConstants.Length500);
        // });
        // 3. OwnsOne + OwnsMany + ToJson
        builder.OwnsOne(u => u.Details, db =>
        {
            db.ToJson("details");
            db.OwnsMany(d => d.SocialNetworks, socialNetworkBuilder =>
            {
                socialNetworkBuilder.Property(sn => sn.Link)
                    .HasJsonPropertyName("link")
                    .IsRequired().HasMaxLength(LengthConstants.Length500);
                socialNetworkBuilder.Property(sn => sn.Name)
                    .HasJsonPropertyName("name")
                    .IsRequired().HasMaxLength(LengthConstants.Length500);
            });
            db.Property(d => d.FIO).IsRequired().HasJsonPropertyName("fio");
            db.Property(d => d.Description).IsRequired().HasJsonPropertyName("description");
        });
        // 4. HasConversion (less efficient)
        // builder.Property(u => u.Details)
        //     .HasConversion(
        //         v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
        //         v => JsonSerializer.Deserialize<Details>(v, JsonSerializerOptions.Default)!)
        //     .HasColumnType("jsonb");
    }
}