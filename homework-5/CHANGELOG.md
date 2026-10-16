# Changelog

Все значимые изменения документируются по формату Keep a Changelog.

## [1.9.0] - 2026-10-17

### Added

- Добавлены теги `jira.issue`, `campus.auditory_id`, `campus.user_id` и
  `campus.booking_id` для бронирования аудиторий.
- Добавлены три фазы миграции Capacity и dual write в C#.
- Добавлен xUnit sanity-тест бронирования на одну минуту с Cleanup.

### Fixed

- Проверка интервала бронирования не допускает окончание раньше начала.

### Breaking Changes

- После фазы Contract `Auditoriums.Capacity` имеет тип `integer`; старая версия
  сервиса должна быть полностью выведена из эксплуатации до этой фазы.
