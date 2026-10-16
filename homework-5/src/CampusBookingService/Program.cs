using CampusBookingService.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton<IBookingService, InMemoryBookingService>();
builder.Services.AddSingleton<IAuditoriumCapacityStore, InMemoryAuditoriumCapacityStore>();

var app = builder.Build();
app.MapGet("/health", () => Results.Ok(new { status = "ok", release = "v1.9.0" }));
app.MapControllers();
app.Run();

public partial class Program;
