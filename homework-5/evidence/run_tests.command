#!/bin/zsh
cd /Users/aleksandr/Desktop/Практические_задания/CampusHomework5.Bortsov
printf '\033[3J\033[H\033[2J'
printf 'ПРАКТИЧЕСКАЯ РАБОТА №5 — ФАКТИЧЕСКИЙ ЗАПУСК XUNIT\n'
printf 'Проект: CampusHomework5.Bortsov | CAMPUS-102 | v1.9.0\n\n'
dotnet test homework-5/tests/CampusBookingService.Tests/CampusBookingService.Tests.csproj --configuration Release --logger "console;verbosity=normal"
