namespace SeatReservation.Domain.Reservations;

public class Reservation
{
    // ReservedSeats и Reservation M to M. Поэтому м создать List<Seat> ReservedSeats тут и List<Reservation> в Reservation
    // public List<Seat> ReservedSeats { get; private set; } = [];
    // Но, это создаст один модуль Reservation + Seat + Venue, а у нас будет Reservation отдельный процесс.
    // Поэтому создадим ReservationSeat отдельный класс, который будет в этом модуле связующей таблицей

    private List<ReservationSeat> _reservedSeats;

    public Reservation(Guid id, Guid eventId, Guid userId, IEnumerable<Guid> seatIds)
    {
        Id = id;
        EventId = eventId;
        UserId = userId;
        Status = ReservationStatus.Pending;
        CreatedAt = DateTime.UtcNow;

        var reservedSeats = seatIds
            .Select(seatId => new ReservationSeat(Guid.NewGuid(), this, seatId))
            .ToList();
        _reservedSeats = reservedSeats;
    }

    public Guid Id { get; }
    public Guid EventId { get; private set; }
    public Guid UserId { get; private set; }
    public ReservationStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public IReadOnlyList<ReservationSeat> ReservedSeats => _reservedSeats;
}