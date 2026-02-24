namespace SeatReservation.Domain.Users;

public class User
{
    public User() {}

    public Guid Id { get; set; }
    public Details Details { get; set; }
}

public record Details
{
    public Details() { }

    public string Description { get; set; }
    public string FIO { get; set; }
    public IReadOnlyCollection<SocialNetwork> SocialNetworks { get; set; }
}

public record SocialNetwork
{
    public SocialNetwork() { }

    public string Name { get; init; }
    public string Link { get; init; }
}