namespace DesignPatternsPlayground;

public sealed record CheckoutRequest(
    string UserId,
    int ProductId,
    int Quantity,
    string NotificationChannel);

public sealed record CheckoutResult(bool Success, string Message);

public sealed record Product(int Id, string Name, decimal UnitPrice);
