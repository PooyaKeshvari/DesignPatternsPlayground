using DesignPatternsPlayground;

Console.WriteLine("=== Design Patterns Playground ===");

IProductRepository productRepository = new InMemoryProductRepository();
IPriceService priceService = new PriceService(productRepository);
priceService = new CachedPriceServiceDecorator(priceService);

var notificationFactory = new NotificationFactory();
var discountFactory = new DiscountPolicyFactory();
var orderFacade = new OrderFacade(priceService, notificationFactory, discountFactory);

var checkoutWithWallet = new CheckoutService(PaymentStrategyFactory.Create("wallet"), orderFacade);
var checkoutWithCard = new CheckoutService(PaymentStrategyFactory.Create("card"), orderFacade);

var firstResult = checkoutWithWallet.Checkout(new CheckoutRequest("USR-001", 1, 2, "email"));
Console.WriteLine(firstResult.Message);

var secondResult = checkoutWithCard.Checkout(new CheckoutRequest("USR-002", 1, 1, "sms"));
Console.WriteLine(secondResult.Message);

var thirdResult = checkoutWithCard.Checkout(new CheckoutRequest("USR-003", 1, 1, "email", "SAVE10"));
Console.WriteLine(thirdResult.Message);

var fourthResult = checkoutWithWallet.Checkout(new CheckoutRequest("USR-004", 2, 2, "sms", "VIP20"));
Console.WriteLine(fourthResult.Message);

Console.WriteLine("Done.");
