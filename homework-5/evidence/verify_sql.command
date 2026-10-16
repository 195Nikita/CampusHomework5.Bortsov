#!/bin/zsh
printf '\033[3J\033[H\033[2J'
printf 'ПРАКТИЧЕСКАЯ РАБОТА №5 — ПРОВЕРКА МИГРАЦИЙ POSTGRESQL\n'
printf 'Фазы 01_expand.sql, 02_migrate.sql и 03_contract.sql выполнены\n\n'
docker exec campus-pr5-postgres psql -U postgres -d campus -c "SELECT column_name, data_type, is_nullable FROM information_schema.columns WHERE table_name='Auditoriums' ORDER BY ordinal_position; SELECT * FROM \"Auditoriums\" ORDER BY \"Id\";"
