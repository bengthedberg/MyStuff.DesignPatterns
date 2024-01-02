// See https://aka.ms/new-console-template for more information
Console.WriteLine("Processing payment using Credit Card:");
var creditCardFactory = new CreditCardPaymentFactory();
var creditCardProcessor = new PaymentProcessor(creditCardFactory);
creditCardProcessor.ProcessPayment(100.00M);
Console.WriteLine("\nProcessing payment using PayPal:");
var payPalFactory = new PayPalPaymentFactory();
var payPalProcessor = new PaymentProcessor(payPalFactory);
payPalProcessor.ProcessPayment(100.00M);


// Abstract Products
public interface IPaymentAuthorization
{
    bool AuthorizePayment(decimal amount);
}
public interface IPaymentTransfer
{
    bool Transfer(decimal amount);
}
// Concrete Products for Credit Card
public class CreditCardAuthorization : IPaymentAuthorization
{
    public bool AuthorizePayment(decimal amount)
    {
        Console.WriteLine($"Authorizing payment of {amount} via Credit Card...");
        return true; // Mocked success
    }
}
public class CreditCardTransfer : IPaymentTransfer
{
    public bool Transfer(decimal amount)
    {
        Console.WriteLine($"Transferring payment of {amount} via Credit Card...");
        return true; // Mocked success
    }
}
// Concrete Products for PayPal
public class PayPalAuthorization : IPaymentAuthorization
{
    public bool AuthorizePayment(decimal amount)
    {
        Console.WriteLine($"Authorizing payment of {amount} via PayPal...");
        return true; // Mocked success
    }
}
public class PayPalTransfer : IPaymentTransfer
{
    public bool Transfer(decimal amount)
    {
        Console.WriteLine($"Transferring payment of {amount} via PayPal...");
        return true; // Mocked success
    }
}
// Abstract Factory
public interface IPaymentFactory
{
    IPaymentAuthorization CreateAuthorization();
    IPaymentTransfer CreateTransfer();
}
// Concrete Factories
public class CreditCardPaymentFactory : IPaymentFactory
{
    public IPaymentAuthorization CreateAuthorization() => new CreditCardAuthorization();
    public IPaymentTransfer CreateTransfer() => new CreditCardTransfer();
}
public class PayPalPaymentFactory : IPaymentFactory
{
    public IPaymentAuthorization CreateAuthorization() => new PayPalAuthorization();
    public IPaymentTransfer CreateTransfer() => new PayPalTransfer();
}
// Client Code
public class PaymentProcessor
{
    private readonly IPaymentAuthorization _authorization;
    private readonly IPaymentTransfer _transfer;
    public PaymentProcessor(IPaymentFactory factory)
    {
        _authorization = factory.CreateAuthorization();
        _transfer = factory.CreateTransfer();
    }
    public bool ProcessPayment(decimal amount)
    {
        if (_authorization.AuthorizePayment(amount))
        {
            return _transfer.Transfer(amount);
        }
        return false;
    }
}
