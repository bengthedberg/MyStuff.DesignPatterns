# Factory Methods

Objects can instantiate other objects directly by calling the new keyword, but this approach is often not the best choice.

Instantiating the object is a separate responsibility that should be carried over from the client code to separate classes/methods called factories.

Using a Factory design pattern has the following benefits:

The client code has one less responsibility because it doesn’t need to create objects. This is the responsibility of the factory.
Factory encapsulates the logic of creating an object that can be reused by many clients.
There are many variations of a Factory pattern, each of which solves a different problem.

## Static Factory Method

The constructor has the same name as the class in which it is declared. If there are multiple overloaded constructors in the class, they will all have the same name.

Client code that needs to instantiate a class with multiple overloaded constructors may not know which constructor to call without examining the implementation details of the class.

Also, sometimes constructors don’t work at all when they need to have the same number of parameters of the same type.

The following code won’t compile:

```csharp 
public struct Duration
{
    public Duration(double seconds)
    {
    }    
    public Duration(double minutes)
    {   
    }
}
```

Static factory methods are a good alternative to constructors because they have a unique name that can accurately describe the purpose of the returned instance.

```csharp 
public class Duration 
{
    // Properties
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }

    // Static factory methods
    public static Duration DurationInSeconds(double seconds)
        => new Duration(seconds);

    public static Duration DurationInMinutes(double minutes)
        => new Duration(minutes * 60);

    // Private constructor
    private Duration(double seconds)
    {
        this.StartDate = DateTime.UtcNow;
        this.EndDate = this.StartDate.AddSeconds(seconds);
    }
}
```

## Asynchronuos Factory Method

If a class constructor requires a call to an asynchronous initialisation then you can not place this logic inside the constructor. 

The constructor will only return an instance of the class, you can not add the async attribute or return type of Task.

But you can implement an asynchronuos factory method in the class. To ensure that the class is used correctly then any constructors should be made private as well as any asynchronous instantiate methods.


```csharp 

// This class ensure that all users will instantiate the class fully and in an asynchronuos manner.
public class Foo
{
    // Make ctor private so no one can misuse the class as it needs to be initialised.
    private Foo() {}

    // Also make the initialise code private to prevent misuse.
    private async Task<Foo> InitAsync()
    {
        await Task.Delay(1000);
        return this;
    }

    // Asynchronuos Factory Method
    // var x = await Foo.CreateAsync();
    public static Task<foo> CreateAsync()
    {
        var result = new Foo();
        return result.InitAsync();
    }
}

```

## Factory 

Smaller/simpler classes can use the factory method to create instances, but if the class is more complex then it can be recommended to use a seperate class for the factory logic. 

Seperate the responsibility of the class itself from its creation can improve readability and reusability.

```csharp

public class Point
{ 
    public readonly double x, y;
    private Point(double x, double y)
    {
        this.x = x;
        this.y = y;
    }
    public static class PointFactory
    {
        public static Point NewCartesianPoint(double x, double y)
            => new Point(x, y);
        public static Point NewPolarPoint(double rho, double theta)
            => new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
    }
}

```

The factory class is placed inside the `Point` class as its constructor is private to avoid any misuse. 

If this is a library or seperate assemply in your project you could declare the constructiore internal and the move the factory outside the `Point` class.
 
```csharp

public class Point
{ 
    public readonly double x, y;
    internal Point(double x, double y)
    {
        this.x = x;
        this.y = y;
    }
}

public static class PointFactory
{
    public static Point NewCartesianPoint(double x, double y)
        => new Point(x, y);
    public static Point NewPolarPoint(double rho, double theta)
        => new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
}

```

## Abstract Factory

 The Abstract Factory Design Pattern provides an interface for creating families of related or dependent objects without specifying their concrete class names. We can also say that Abstract Factory is a super factory or a factory of factories. That means it prevents the client from knowing which factory would be returned from the abstract factory.    

 Consider a scenario where financial software needs to process payments using different methods, such as “Credit Card” and “PayPal”. The Abstract Factory pattern can help create families of related objects to process payments with each method, considering operations like payment authorization and transfer.

 ```csharp 
 using System;
namespace AbstractFactoryDesignPattern
{
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
    
    // Testing the Abstract Factory Design Pattern
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Processing payment using Credit Card:");
            var creditCardFactory = new CreditCardPaymentFactory();
            var creditCardProcessor = new PaymentProcessor(creditCardFactory);
            creditCardProcessor.ProcessPayment(100.00M);

            Console.WriteLine("\nProcessing payment using PayPal:");
            var payPalFactory = new PayPalPaymentFactory();
            var payPalProcessor = new PaymentProcessor(payPalFactory);
            payPalProcessor.ProcessPayment(100.00M);

            Console.ReadKey();
        }
    }
}
 ```


