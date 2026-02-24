using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SeatReservation.Domain.Events;

namespace SeatReservation.Infrastructure.Postgres.Converters;

public class EventInfoConverter : ValueConverter<IEventInfo, string>
{
    public EventInfoConverter() : base(i => InfoToString(i), s => StringToInfo(s)) { }
    private static string InfoToString(IEventInfo info) => info switch
    {
        ConcertInfo с => $"Concert: {с.Performer}",
        ConferenceInfo с => $"Conference: {с.Speaker} | {с.Topic}",
        OnlineInfo с => $"Online: {с.Url}",
        _ => throw new ArgumentException("Unknown event info type")
    };

    private static IEventInfo StringToInfo(string info)
    {
        var split = info.Split(":", 2);
        var type = split[0].Trim();
        var data = split[1].Trim();

        return type switch
        {
            "Concert" => new ConcertInfo(data),
            "Conference" => new ConferenceInfo(data.Split('|')[0], data.Split('|')[1]),
            "Online" => new OnlineInfo(data),
            _ => throw new ArgumentException("Unknown event info type")
        };
    }
}