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

Версия: 2.0.0 (схема PostgreSQL и слой EF Core). Автор: DementevMaxim.
Дата последнего изменения: 2026-10-19.

Решение BookingMicroService.sln: API, Application, Contracts, Domain, Infrastructure.
Целевая платформа: .NET 8; существующий слой хранения использует EF Core 8.0.11 и PostgreSQL.
Сборка: `dotnet build BookingMicroService.sln`. Тестовых проектов пока нет.
API пока является библиотекой, полноценный запускаемый HTTP-сервис ещё не реализован.

Исходные наработки перенесены из локального BookingMicroService. По согласованию устаревшая резервная копия удалена.
Основной origin текущего репозитория — https://gitlab.com/airline-booking/planning.git.

Booking хранит заказы, снимки пассажиров/маршрутов/рейсов, позиции заказа, резервы,
выпущенные билеты и связи билетов с сегментами. Внутренние идентификаторы — long/bigint.
Внешние идентификаторы поставщиков остаются строками, технические CorrelationId/EventId
сохраняют существующий Guid-формат. Аутентификация относится к внешнему сервису;
UserId — только идентификатор владельца, переданный доверенным вызывающим кодом.
Текущая модель предложения содержит одного поставщика на заказ, несколько попыток резервирования.
Модель нескольких поставщиков и гостевых заказов здесь не реализована.

Локально установлен PostgreSQL 17.11 без прав администратора, без службы Windows и Docker.
Адрес: 127.0.0.1:5432; БД: booking_db; роль: booking_owner.
Данные: %LOCALAPPDATA%\BookingMicroService\pgdata17.
Журнал: %LOCALAPPDATA%\BookingMicroService\postgresql.log.
Сервер запускается от текущего пользователя; автоматический запуск при входе не настроен.

Управление из PowerShell: `& ./scripts/Local-Postgres.ps1 -Action Start` (Stop/Status/Connect).
`& ./scripts/Local-Postgres.ps1 -Action Configure` устанавливает строку подключения только
для текущего терминала и запускаемых из него процессов, без печати пароля.
Пароли — зашифрованные Windows DPAPI credentials вне репозитория, в
%LOCALAPPDATA%\BookingMicroService. Для получения PSCredential текущего пользователя:
`$credential = & ./scripts/Local-Postgres.ps1 -Action Credential`.
Для ручного подключения pgAdmin пароль можно посмотреть локально через
`$credential.GetNetworkCredential().Password`; не публикуйте этот вывод.

Подключение EF: ConnectionStrings:BookingDatabase либо BOOKING_DATABASE_CONNECTION_STRING.
Формат: Host=127.0.0.1;Port=5432;Database=booking_db;Username=booking_owner;Password=<локальный секрет>.
API остаётся библиотекой; будущий HTTP-host должен вызвать `services.AddBookingDatabase(configuration)`.
Пароли по умолчанию и автоматическое создание БД при старте отсутствуют.

Схема: Infrastructure/Persistence/Sql/001_booking_schema.sql, проверки: verify_schema.sql.
SQL предназначен только для пустой БД и применяется транзакционно, без IF NOT EXISTS.
В локальной booking_db создано 11 таблиц; SQL-проверки корректной цепочки заказа,
межзаказных FK, повторных резервов/событий/билетов, статусов и денежных сумм прошли.
Тестовые данные откатились, заказов в базе нет.
Ограничения защищают связи внутри заказа, суммы, статусы и ключи повторных операций.
Inbox/outbox — только модель хранения: обработчики, повторные доставки и интеграционные вызовы
пока не реализованы. Паспортное поле ожидает ciphertext; шифрование должно выполняться приложением.

По поручению пользователя сборка и EF tooling не запускались. Скомпилированная модель EF
и её соответствие SQL ещё не проверены. Схема подготовлена вручную, EF-миграции и ModelSnapshot
не создавались. После разрешения сборки сначала проверить модель, сгенерировать InitialBookingSchema
и сравнить SQL, затем согласовать baseline существующей локальной БД. Не применять начальную
миграцию поверх уже созданных таблиц и не использовать EnsureCreated совместно с миграциями.
Изменения сохранены в feature/booking-postgresql; до проверки сборки слияние и push не выполняются.

Удалены старые дубли контекстов, неподключённый черновик consumer Search с отсутствующими
типами и локальная сущность User, которая ошибочно копировала модель UserService.
Эти исходники сохранены в истории Git (коммит импорта 74de010).

Версия задаётся в Directory.Build.props. Правовые документы: legal/EULA.txt и legal/NOTICE.txt;
они копируются к результатам сборки и публикации. Ключи подписи исключены из Git.
