namespace CampusBookingService.Models;

public sealed record CreateBookingRequest(
    Guid UserId,
    Guid AuditoriumId,
    DateTimeOffset StartsAt,
    DateTimeOffset EndsAt);

public sealed record Booking(
    Guid Id,
    Guid UserId,
    Guid AuditoriumId,
    DateTimeOffset StartsAt,
    DateTimeOffset EndsAt,
    DateTimeOffset CreatedAt);

public sealed record AuditoriumCapacity(
    Guid AuditoriumId,
    short Capacity,
    int? CapacityInteger);
