namespace SeatReservation.Domain.Events;

public class EventDetails(int capacity, string description)
{
    // Event и EventDetails 1 to 1. Поэтому Id будет и Primary и Foreign
    public Guid EventId { get; } = Guid.Empty;
    public int Capacity { get; private set; } = capacity;
    public string Description { get; private set; } = description;
}