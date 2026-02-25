namespace DesignPatternsPlayground;

public interface IPaymentStrategy
{
    bool Pay(decimal amount, string userId);
}

public sealed class WalletPaymentStrategy : IPaymentStrategy
{
    public bool Pay(decimal amount, string userId)
    {
        Console.WriteLine($"WalletPaymentStrategy: paid {amount:C} for {userId}.");
        return true;
    }
}

public sealed class CardPaymentStrategy : IPaymentStrategy
{
    public bool Pay(decimal amount, string userId)
    {
        Console.WriteLine($"CardPaymentStrategy: charged {amount:C} for {userId}.");
        return true;
    }
}

public static class PaymentStrategyFactory
{
    public static IPaymentStrategy Create(string paymentType)
    {
        return paymentType.ToLowerInvariant() switch
        {
            "wallet" => new WalletPaymentStrategy(),
            "card" => new CardPaymentStrategy(),
            _ => throw new NotSupportedException($"Payment type '{paymentType}' is not supported.")
        };
    }
}
