# Практическая работа №5 — безопасный релиз CAMPUS-102


`homework-5/`.

## Проверка

```bash
dotnet test homework-5/tests/CampusBookingService.Tests/CampusBookingService.Tests.csproj --configuration Release
dotnet run --project homework-5/src/CampusBookingService/CampusBookingService.csproj --urls http://127.0.0.1:5090
```

После запуска доступны `GET /health`, `POST /api/bookings` и
`DELETE /api/bookings/{id}`.
