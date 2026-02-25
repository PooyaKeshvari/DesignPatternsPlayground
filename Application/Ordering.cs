namespace DesignPatternsPlayground;

public sealed class OrderFacade
{
    private readonly IPriceService _priceService;
    private readonly NotificationFactory _notificationFactory;

    public OrderFacade(IPriceService priceService, NotificationFactory notificationFactory)
    {
        _priceService = priceService;
        _notificationFactory = notificationFactory;
    }

    public CheckoutResult PlaceOrder(CheckoutRequest request, IPaymentStrategy paymentStrategy)
    {
        decimal unitPrice = _priceService.GetUnitPrice(request.ProductId);
        decimal total = unitPrice * request.Quantity;

        bool paid = paymentStrategy.Pay(total, request.UserId);
        if (!paid)
        {
            return new CheckoutResult(false, "Payment failed.");
        }

        INotifier notifier = _notificationFactory.Create(request.NotificationChannel);
        notifier.Notify(request.UserId, $"Your order is confirmed. Total: {total:C}");

        return new CheckoutResult(true, $"Order for {request.UserId} succeeded. Total: {total:C}");
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
