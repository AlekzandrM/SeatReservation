using CSharpFunctionalExtensions;
using Shared;

namespace SeatReservation.Domain.Venues;

public record VenueId(Guid Value){}

public class Venue
{
    private List<Seat> _seats = [];

    // EF Core
    private Venue(){}
    public Venue(VenueId id, VenueName name, int seatsLimit, IEnumerable<Seat> seats)
    {
        Id = id;
        Name = name;
        SeatsLimit = seatsLimit;
        _seats = seats.ToList();
    }

    public VenueId Id { get; } = null!;
    public VenueName? Name { get; private set; }
    public int SeatsLimit { get; private set; }

    public int SeatsCount => _seats.Count;
    public IReadOnlyList<Seat> Seats => _seats;

    public UnitResult<Error> AddSeat(Seat seat)
    {
        if (SeatsCount >= SeatsLimit)
        {
            return Error.Conflict( "Venue has reached the maximum number of seats" );
        }

        _seats.Add(seat);
        return UnitResult.Success<Error>();
    }

    public void ExpandSeatsLimit(int newSeatsLimit) => SeatsLimit = newSeatsLimit;
}