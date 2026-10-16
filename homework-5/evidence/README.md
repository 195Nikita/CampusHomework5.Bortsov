# Проверочные материалы

Снимки в `screenshots/` сделаны штатной утилитой macOS `screencapture` с
фактически открытых окон Terminal, TextEdit и Safari. Изображения не
генерировались и не подменялись макетами.

- `01_xunit_real.png` — реальный запуск `dotnet test` в Release;
- `02_controller_real.png` — исходный файл контроллера в TextEdit;
- `03_health_real.png` — ответ работающего локального сервиса в Safari;
- `04_postgresql_real.png` — результат трех миграций в PostgreSQL 17.

Контейнер `campus-pr5-postgres` был временным и удален после проверки.
Скрипт `verify_sql.command` фиксирует команду контрольного запроса, который
выполнялся до удаления контейнера.
