using Microsoft.EntityFrameworkCore;
using SeatReservation.Domain.Events;
using SeatReservation.Domain.Users;
using SeatReservation.Domain.Venues;
using SeatReservation.Infrastructure.Postgres;
using DomainEventId = SeatReservation.Domain.Events.EventId;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<ReservationServiceDbContext>(_ =>
    new ReservationServiceDbContext(builder.Configuration.GetConnectionString("ReservationServiceDB") ?? throw new InvalidOperationException("Connection string 'ReservationServiceDB' not found.")));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "AuthService"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapPost("/users", async (ReservationServiceDbContext dbContext) =>
{
    var socialNetwork = new SocialNetwork()
    {
        Link = "Test",
        Name = "Facebook"
    };

    dbContext.Add(new User()
    {
        Details = new Details()
        {
            Description = "Test",
            FIO = "FIO1",
            SocialNetworks = [socialNetwork]
        }
    });
    await dbContext.SaveChangesAsync();
});

app.MapGet("/users", async (ReservationServiceDbContext dbContext) =>
{
    return await dbContext.Users.Where(u => u.Details.SocialNetworks.Any(s => s.Link == "Test")).ToListAsync();
});

app.MapPost("/event", async (ReservationServiceDbContext dbContext) =>
{
    dbContext.Add(
        new Event(
            new DomainEventId(Guid.NewGuid()),
            new VenueId(Guid.Parse("d67bfab0-377b-48cc-8a8b-f2ae9fb61762")),
            new EventDetails(10, "Test"),
            "Test",
            DateTime.UtcNow,
            new ConferenceInfo("Aleks", "EF")));

    dbContext.Add(
        new Event(
            new DomainEventId(Guid.NewGuid()),
            new VenueId(Guid.Parse("d67bfab0-377b-48cc-8a8b-f2ae9fb61762")),
            new EventDetails(20, "Test2"),
            "Test2",
            DateTime.UtcNow,
            new OnlineInfo("url")));

    dbContext.Add(
        new Event(
            new DomainEventId(Guid.NewGuid()),
            new VenueId(Guid.Parse("d67bfab0-377b-48cc-8a8b-f2ae9fb61762")),
            new EventDetails(30, "Test3"),
            "Test3",
            DateTime.UtcNow,
            new ConcertInfo("Concert")));

    await dbContext.SaveChangesAsync();
});

app.Run();