namespace DesignPatternsPlayground;

public interface IDiscountPolicy
{
    decimal CalculateDiscount(decimal subtotal, CheckoutRequest request);
}

public sealed class NoDiscountPolicy : IDiscountPolicy
{
    public decimal CalculateDiscount(decimal subtotal, CheckoutRequest request)
    {
        return 0m;
    }
}

public sealed class CouponDiscountPolicy : IDiscountPolicy
{
    private readonly decimal _percent;

    public CouponDiscountPolicy(decimal percent)
    {
        _percent = percent;
    }

    public decimal CalculateDiscount(decimal subtotal, CheckoutRequest request)
    {
        if (subtotal <= 0 || _percent <= 0)
        {
            return 0m;
        }

        return decimal.Round(subtotal * _percent, 2, MidpointRounding.AwayFromZero);
    }
}

public sealed class DiscountPolicyFactory
{
    public IDiscountPolicy Create(string? couponCode)
    {
        if (string.IsNullOrWhiteSpace(couponCode))
        {
            return new NoDiscountPolicy();
        }

        return couponCode.Trim().ToUpperInvariant() switch
        {
            "SAVE10" => new CouponDiscountPolicy(0.10m),
            "VIP20" => new CouponDiscountPolicy(0.20m),
            _ => new NoDiscountPolicy()
        };
    }
}
