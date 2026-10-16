using System.Collections.Concurrent;
using CampusBookingService.Models;

namespace CampusBookingService.Services;

public interface IBookingService
{
    Booking Create(CreateBookingRequest request);
    Booking? Find(Guid id);
    bool Cancel(Guid id);
}

public sealed class InMemoryBookingService : IBookingService
{
    private readonly ConcurrentDictionary<Guid, Booking> _bookings = new();

    public Booking Create(CreateBookingRequest request)
    {
        if (request.EndsAt <= request.StartsAt)
            throw new ArgumentException("Время окончания должно быть позже времени начала.");

        var booking = new Booking(
            Guid.NewGuid(), request.UserId, request.AuditoriumId,
            request.StartsAt, request.EndsAt, DateTimeOffset.UtcNow);
        _bookings[booking.Id] = booking;
        return booking;
    }

    public Booking? Find(Guid id) =>
        _bookings.TryGetValue(id, out var booking) ? booking : null;

    public bool Cancel(Guid id) => _bookings.TryRemove(id, out _);
}
