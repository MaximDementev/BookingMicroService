# Booking draft integration

- Origin: https://gitlab.com/airline-booking/planning.git; default branch: main.
- BookingMicroService.sln contains API, Application, Contracts, Domain and Infrastructure (.NET 8).
- Build: dotnet build BookingMicroService.sln. There is no test project in the imported draft.
- Imported from the local BookingMicroService repository, including the uncommitted BookingContext.cs edit.
- The obsolete sibling backup was deleted at the user's explicit request.
- Existing dependency versions must be preserved. Do not commit .vs, bin, obj or signing secrets.
- Directory.Build.props is the common version source. Legal notices are copied by Directory.Build.targets.
- BookingDbContext is the sole EF context. BookingModelConfiguration maps only local snapshots and orders.
- Schema SQL: Infrastructure/Persistence/Sql/001_booking_schema.sql; SQL smoke checks: verify_schema.sql.
- Internal entity IDs use long/bigint. External provider IDs are opaque strings; transport correlation/event IDs retain their formats.
- Authentication is external. Current order model has one supplier; payment and issuance states are independent.
- Local PostgreSQL 17 runs as the current user on 127.0.0.1:5432; booking_db/booking_owner.
- Runtime/data/DPAPI credentials are outside Git at %LOCALAPPDATA%/BookingMicroService; lifecycle script: scripts/Local-Postgres.ps1.
- Do not build or run EF tooling until the user lifts the current no-build restriction.
- The SQL schema is hand-authored, not an EF migration. No EF snapshot or migration baseline exists yet.
- Retain the feature branch and do not merge/push uncompiled changes.
