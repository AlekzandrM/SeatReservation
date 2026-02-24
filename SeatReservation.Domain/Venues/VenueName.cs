using CSharpFunctionalExtensions;
using Shared;
using Shared.Constants;

namespace SeatReservation.Domain.Venues;

// ValueObject
public record VenueName
{
    private VenueName(string prefix, string name)
    {
        Prefix = prefix;
        Name = name;
    }
    public string Prefix { get; }
    public string Name { get; }

    public override string ToString() => $"{Prefix}-{Name}";

    public static Result<VenueName, Error> Create(string prefix, string name)
    {
        if (string.IsNullOrEmpty(prefix) || string.IsNullOrEmpty(name))
        {
            return Error.Validation("venue.name", "Venue name and prefix must not be empty");
        }
        if (prefix.Length >= LengthConstants.Length50 || name.Length >= LengthConstants.Length500)
        {
            return Error.Validation("venue.name", "Venue name too long");
        }

        return new VenueName(prefix, name);
    }
}