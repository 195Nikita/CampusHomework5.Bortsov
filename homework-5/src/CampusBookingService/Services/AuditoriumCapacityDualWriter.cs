using System.Collections.Concurrent;
using CampusBookingService.Models;

namespace CampusBookingService.Services;

public interface IAuditoriumCapacityStore
{
    Task WriteBothAsync(Guid auditoriumId, short legacyCapacity, int expandedCapacity,
        CancellationToken cancellationToken = default);
    AuditoriumCapacity? Find(Guid auditoriumId);
}

public sealed class InMemoryAuditoriumCapacityStore : IAuditoriumCapacityStore
{
    private readonly ConcurrentDictionary<Guid, AuditoriumCapacity> _rows = new();

    public Task WriteBothAsync(Guid auditoriumId, short legacyCapacity, int expandedCapacity,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _rows[auditoriumId] = new AuditoriumCapacity(
            auditoriumId,
            Capacity: legacyCapacity,
            CapacityInteger: expandedCapacity);
        return Task.CompletedTask;
    }

    public AuditoriumCapacity? Find(Guid auditoriumId) =>
        _rows.TryGetValue(auditoriumId, out var row) ? row : null;
}

public sealed class AuditoriumCapacityDualWriter(IAuditoriumCapacityStore store)
{
    public Task UpdateAsync(Guid auditoriumId, short capacity,
        CancellationToken cancellationToken = default)
    {
        // Expand phase: every write updates the legacy smallint and new integer columns.
        return store.WriteBothAsync(
            auditoriumId,
            legacyCapacity: capacity,
            expandedCapacity: capacity,
            cancellationToken);
    }
}
