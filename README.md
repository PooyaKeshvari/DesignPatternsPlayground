# Design Patterns Playground

Console project that demonstrates common backend design patterns in practical C# flow.

## What This Project Demonstrates
- `Strategy` for payment selection.
- `Factory` for notification channel creation.
- `Decorator` for price caching.
- `Facade` for order orchestration.
- `Repository` abstraction over data access.

## Structure
- `Program.cs`: composition root and scenario runner.
- `Domain/Contracts.cs`: request/response/domain records.
- `Repository/Repository.cs`: product repository abstraction + in-memory implementation.
- `Pricing/Pricing.cs`: core pricing service + cache decorator.
- `Payments/Payments.cs`: payment strategies + strategy factory.
- `Factories/Notifications.cs`: notifier implementations + factory.
- `Application/Ordering.cs`: order facade and checkout service.

## Run
```bash
dotnet run --project src/DesignPatternsPlayground/DesignPatternsPlayground.csproj
```

## Interview Talking Points
- How Strategy keeps payment logic open for extension.
- Why Decorator adds caching without changing base service code.
- How Facade reduces coupling for application consumers.
