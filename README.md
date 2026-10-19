# Airline Booking — Planning & Documentation

Central project repository: backlog, tasks, and links to all team resources.

**Stack:** .NET (microservices) · React + MUI (web) · Kubernetes (deployment)

---

## 📌 Resources

| Resource | Link | Purpose |
|----------|------|---------|
| Excalidraw | [open](https://excalidraw.com/) | Architecture diagrams, sketches |
| Figma | [open](https://www.figma.com/) | Design mockups (web & mobile) |
| Task board | [open](https://gitlab.com/groups/airline-booking/-/boards) | Team kanban board |
| Backlog | [open](https://gitlab.com/groups/airline-booking/-/work_items) | Full list of tasks |

---

## 🗂 Project Structure

- **backend/** — .NET microservices
  - `identity` — authentication, users
  - `flights` — flight search
  - `booking` — bookings
  - `payments` — payments
- **frontend/** — clients
  - `user-ui` — React + MUI
  - `mobile-app` (maybe later)
- **infrastructure/** — deployment, Helm charts, CI templates

---

## 🚀 Getting Started

**Requirements:**
- .NET SDK 8+
- Node.js 20+
- Docker

_Setup instructions will be added as services are created._

---

## 🤝 Team Conventions

**Branches:**
- `main` — stable
- `dev` — integration
- `feature/<issue-number>-short-description` — features

**Commits:** short and clear, with the issue number (`#5`).

**Working with tasks:** each task → its own branch → Merge Request with `Closes #<number>`.
## Локальные наработки BookingMicroService

Версия: 1.1.0 (импорт черновика). Автор: DementevMaxim.
Дата последнего изменения: 2026-10-19.

Решение BookingMicroService.sln: API, Application, Contracts, Domain, Infrastructure.
Целевая платформа: .NET 8; существующий слой хранения использует EF Core 8.0.11 и PostgreSQL.
Сборка: `dotnet build BookingMicroService.sln`. Тестовых проектов пока нет.
API пока является библиотекой, полноценный запускаемый HTTP-сервис ещё не реализован.

Исходные наработки перенесены из локального BookingMicroService, включая незакоммиченное изменение Infrastructure/BookingContext.cs.
Оригинал с полной историей GitHub сохранён рядом в BookingMS.backup-20261019.
Основной origin текущего репозитория — https://gitlab.com/airline-booking/planning.git.

Проверка импорта: 58 файлов скопированы с проверкой SHA-256 до добавления метаданных защиты.
Сборка черновика не проходит: в Domain/Entities/User.cs отсутствуют UserStatus, UserCredential,
UserSession, UserPushDevice и NotificationPreference. В Infrastructure/BookingContext.cs также
остался незавершённый черновик конфигурации EF. Исправление поведения не входит в перенос.
Импорт оставлен в feature/import-booking-drafts, слияние в main и push не выполняются до успешной проверки.

Версия задаётся в Directory.Build.props. Правовые документы: legal/EULA.txt и legal/NOTICE.txt;
они копируются к результатам сборки и публикации. Ключи подписи исключены из Git.
