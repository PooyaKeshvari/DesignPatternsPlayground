namespace DesignPatternsPlayground;

public sealed class OrderFacade
{
    private readonly IPriceService _priceService;
    private readonly NotificationFactory _notificationFactory;
    private readonly DiscountPolicyFactory _discountPolicyFactory;

    public OrderFacade(
        IPriceService priceService,
        NotificationFactory notificationFactory,
        DiscountPolicyFactory discountPolicyFactory)
    {
        _priceService = priceService;
        _notificationFactory = notificationFactory;
        _discountPolicyFactory = discountPolicyFactory;
    }

    public CheckoutResult PlaceOrder(CheckoutRequest request, IPaymentStrategy paymentStrategy)
    {
        decimal unitPrice = _priceService.GetUnitPrice(request.ProductId);
        decimal subtotal = unitPrice * request.Quantity;
        var discountPolicy = _discountPolicyFactory.Create(request.CouponCode);
        decimal discount = discountPolicy.CalculateDiscount(subtotal, request);
        decimal total = Math.Max(0m, subtotal - discount);

        bool paid = paymentStrategy.Pay(total, request.UserId);
        if (!paid)
        {
            return new CheckoutResult(false, "Payment failed.");
        }

        INotifier notifier = _notificationFactory.Create(request.NotificationChannel);
        notifier.Notify(
            request.UserId,
            $"Your order is confirmed. Subtotal: {subtotal:C}, Discount: {discount:C}, Final: {total:C}");

        return new CheckoutResult(
            true,
            $"Order for {request.UserId} succeeded. Subtotal: {subtotal:C}, Discount: {discount:C}, Final: {total:C}");
    }
}

public sealed class CheckoutService
{
    private readonly IPaymentStrategy _paymentStrategy;
    private readonly OrderFacade _orderFacade;

    public CheckoutService(IPaymentStrategy paymentStrategy, OrderFacade orderFacade)
    {
        _paymentStrategy = paymentStrategy;
        _orderFacade = orderFacade;
    }

    public CheckoutResult Checkout(CheckoutRequest request)
    {
        return _orderFacade.PlaceOrder(request, _paymentStrategy);
    }
}
