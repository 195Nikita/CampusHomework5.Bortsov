# Практическая работа №5

**Студент:** Борцов Никита Александрович  
**Группа:** И-14-23-9  
**Вариант:** 2  
**Модуль:** Бронирование аудиторий  
**Задача:** CAMPUS-102

## Ссылки на материалы

- [C#-контроллер](src/CampusBookingService/Controllers/BookingController.cs)
- [Dual write вместимости](src/CampusBookingService/Services/AuditoriumCapacityDualWriter.cs)
- [01_expand.sql](migrations/01_expand.sql)
- [02_migrate.sql](migrations/02_migrate.sql)
- [03_contract.sql](migrations/03_contract.sql)
- [Sanity-тест](tests/CampusBookingService.Tests/ProductionSanityTests.cs)
- [Change Request v1.9.0](docs/CR-v1.9.0.md)
- [Branch Protection Rules](docs/branch-protection.md)
- [ADR 0009](docs/adr/0009-capacity-expand-contract.md)
- [CHANGELOG.md](CHANGELOG.md)

## Change Request

Релиз v1.9.0 запланирован на 17 октября 2026 года с 02:00 до 02:30 МСК —
период минимального учебного трафика. В релиз входят OpenTelemetry-теги,
dual write и безопасное расширение типа Capacity. Выпуск проводится по фазам
Expand, Migrate и Contract. При превышении порогов ошибок или неуспешном
sanity-тесте приложение откатывается командой
`kubectl rollout undo deployment/campus-booking-service -n production`.

## Результат

xUnit-проверки подтверждают установку trace-тегов, создание бронирования на одну
минуту, обязательную очистку записи и одновременную запись вместимости в старое
и новое поля.
