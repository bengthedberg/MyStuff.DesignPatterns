# Singleton

Ensure that a class only has one instance of it.

## Motivation

Singleton has a bad reputation as its use can be a code smell; poor design of the application. 

Though there are some cases where a singelton is useful:

* Database repositories 
* Object Factory Classes

So places where the creating of the class is quite expensive or you only really need one instance. That instance is also shared to everything that needs it.

Ideally the instance would use lazy instantiation; any resources in the singleton are only created when needed.

You also want to prevent any additional instances of the class, as well as being thread safe.

## Implementation

A Singleton class in C#, at its core, is made up of three main components.

* A private static instance of the class itself.
* A private constructor.
* A public static method that returns the instance of the class.

```csharp
public sealed class Singleton
{
    private static Singleton instance = null;  // Private Instance
    private Singleton() {} // Private Constructor 

    public static Singleton Instance  => instance;
}
```

### Lazy Loading

Use Lazy Loading to save resources and improve performance by deferring the initialization of expensive objects until they're requested. 


```csharp
public class Singleton
{
    private static readonly Lazy<Singleton> lazyInstance =
        new Lazy<Singleton>(() => new Singleton());

    private Singleton(){}

    public static Singleton Instance => lazyInstance.Value;
}
```

Note that lazy loading occurs when you access the Lazy<T>.Value property.

In the above case when youaccess the `Singleton.Instance` property.

## Singleton by thread

You can use `ThreadLocal` instead of `Lazy` to turn the singleton to thread scope. Each thread gets its own singleton instance.
