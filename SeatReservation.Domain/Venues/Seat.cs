using CSharpFunctionalExtensions;
using Shared;

namespace SeatReservation.Domain.Venues;

public class Seat
{

    public Seat(Guid id, int rowNumber, int seatNumber)
    {
        Id = id;
        SeatNumber = seatNumber;
        RowNumber = rowNumber;
    }
    public Guid Id { get; }
    public int RowNumber { get; private set;  }
    public int SeatNumber { get; private set; }

    public static Result<Seat, Error> Create(int rowNumber, int seatNumber)
    {
        if (rowNumber < 1 || seatNumber < 1)
        {
            return Error.Validation("seat.rowNumber", "Row number and seat number must be greater than 0");
        }

        return new Seat(Guid.NewGuid(), rowNumber, seatNumber);
    }
}