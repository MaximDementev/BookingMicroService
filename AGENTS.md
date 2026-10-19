# Booking draft integration

- Origin: https://gitlab.com/airline-booking/planning.git; default branch: main.
- BookingMicroService.sln contains API, Application, Contracts, Domain and Infrastructure (.NET 8).
- Build: dotnet build BookingMicroService.sln. There is no test project in the imported draft.
- Imported from the local BookingMicroService repository, including the uncommitted BookingContext.cs edit.
- Original repository and its complete Git history are preserved in sibling BookingMS.backup-20261019.
- Existing dependency versions must be preserved. Do not commit .vs, bin, obj or signing secrets.
- Directory.Build.props is the common version source. Legal notices are copied by Directory.Build.targets.
- Draft import must remain on its feature branch while the solution fails to build.
