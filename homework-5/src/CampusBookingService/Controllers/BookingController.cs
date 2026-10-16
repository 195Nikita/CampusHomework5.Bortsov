using System.Diagnostics;
using CampusBookingService.Models;
using CampusBookingService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CampusBookingService.Controllers;

[ApiController]
[Route("api/bookings")]
public sealed class BookingController(IBookingService bookings) : ControllerBase
{
    [HttpPost]
    public ActionResult<Booking> Create([FromBody] CreateBookingRequest request)
    {
        Activity.Current?.SetTag("jira.issue", "CAMPUS-102");
        Activity.Current?.SetTag("campus.auditory_id", request.AuditoriumId.ToString());
        Activity.Current?.SetTag("campus.user_id", request.UserId.ToString());

        var booking = bookings.Create(request);
        Activity.Current?.SetTag("campus.booking_id", booking.Id.ToString());
        return CreatedAtAction(nameof(Get), new { id = booking.Id }, booking);
    }

    [HttpGet("{id:guid}")]
    public ActionResult<Booking> Get(Guid id)
    {
        var booking = bookings.Find(id);
        return booking is null ? NotFound() : Ok(booking);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Cancel(Guid id) =>
        bookings.Cancel(id) ? NoContent() : NotFound();
}
