namespace DesignPatternsPlayground;

public sealed record CheckoutRequest(
    string UserId,
    int ProductId,
    int Quantity,
    string NotificationChannel,
    string? CouponCode = null);

public sealed record CheckoutResult(bool Success, string Message);

public sealed record Product(int Id, string Name, decimal UnitPrice);
