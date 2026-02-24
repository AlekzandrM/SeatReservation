
using SeatReservation.Domain.Venues;

namespace SeatReservation.Domain.Events;

public record EventId(Guid Value){}

public class Event
{
    private Event(){}
    public Event(EventId id, VenueId venueId, EventDetails details, string name, DateTime eventDate, IEventInfo info)
    {
        Id = id;
        VenueId = venueId;
        Details = details;
        Name = name;
        EventDate = eventDate;
        Info = info;
    }
    public EventId Id { get; private set; }
    public EventDetails Details { get; private set; }
    public VenueId VenueId { get; private set; }
    public string Name { get; private set; }
    public EventType Type { get; private set; }
    public DateTime EventDate { get; private set; }
    public IEventInfo Info { get; private set; }
}

public enum EventType
{
    Concert,
    Conference,
    Online
}
public interface IEventInfo {}
public record ConcertInfo(string Performer) : IEventInfo;
public record ConferenceInfo(string Speaker, string Topic) : IEventInfo;
public record OnlineInfo(string Url) : IEventInfo;
