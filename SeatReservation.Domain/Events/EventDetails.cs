namespace SeatReservation.Domain.Events;

public class EventDetails(int capacity, string description)
{
    // Event and EventDetails is one-to-one. So, EventId Primary and Foreign key
    public EventId EventId { get; private set; } = null!;
    public int Capacity { get; private set; } = capacity;
    public string Description { get; private set; } = description;
}