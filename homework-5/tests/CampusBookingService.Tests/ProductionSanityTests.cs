using System.Diagnostics;
using CampusBookingService.Controllers;
using CampusBookingService.Models;
using CampusBookingService.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CampusBookingService.Tests;

public sealed class ProductionSanityTests
{
    [Fact]
    public void OneMinuteBooking_IsTraced_AndAlwaysCleanedUp()
    {
        var service = new InMemoryBookingService();
        var controller = new BookingController(service);
        var userId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var auditoriumId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        Booking? createdBooking = null;

        try
        {
            var startsAt = DateTimeOffset.Parse("2026-10-17T02:10:00+03:00");
            var request = new CreateBookingRequest(
                userId, auditoriumId, startsAt, startsAt.AddMinutes(1));

            using var activity = new Activity("POST /api/bookings").Start();
            var response = controller.Create(request);

            Assert.Equal("CAMPUS-102", activity.GetTagItem("jira.issue"));
            Assert.Equal(auditoriumId.ToString(), activity.GetTagItem("campus.auditory_id"));
            Assert.Equal(userId.ToString(), activity.GetTagItem("campus.user_id"));

            var created = Assert.IsType<CreatedAtActionResult>(response.Result);
            createdBooking = Assert.IsType<Booking>(created.Value);
            Assert.Equal(TimeSpan.FromMinutes(1), createdBooking.EndsAt - createdBooking.StartsAt);
            Assert.NotNull(service.Find(createdBooking.Id));
        }
        finally
        {
            if (createdBooking is not null)
                service.Cancel(createdBooking.Id);
        }

        Assert.NotNull(createdBooking);
        Assert.Null(service.Find(createdBooking!.Id));
    }

    [Fact]
    public async Task ExpandPhase_DualWritesLegacyAndIntegerCapacity()
    {
        var store = new InMemoryAuditoriumCapacityStore();
        var writer = new AuditoriumCapacityDualWriter(store);
        var auditoriumId = Guid.Parse("22222222-2222-2222-2222-222222222222");

        await writer.UpdateAsync(auditoriumId, 320);

        var row = Assert.IsType<AuditoriumCapacity>(store.Find(auditoriumId));
        Assert.Equal((short)320, row.Capacity);
        Assert.Equal(320, row.CapacityInteger);
    }
}
