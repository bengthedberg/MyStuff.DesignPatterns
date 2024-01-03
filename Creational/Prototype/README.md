# Prototype Pattern 

Copy existing objects without making your code dependent on their classes.

Say you have an object, and you want to create an copy of it. How would you do it? 

First, you have to create a new object of the same class. Then you have to go through all the fields of the original object and copy their values over to the new object.

Nice! But there’s a catch. Not all objects can be copied that way because some of the object’s fields may be private and not visible from outside of the object itself.

 Since you have to know the object’s class to create a duplicate, **your code becomes dependent on that class**. If the extra dependency doesn’t scare you, there’s another catch. Sometimes you **only know the interface that the object follows, but not its concrete class**, when, for example, a parameter in a method accepts any objects that follow some interface.

## Solution

The Prototype pattern delegates the cloning process to the actual objects that are being cloned. The pattern declares a common interface for all objects that support cloning. This interface lets you clone an object without coupling your code to the class of that object. Usually, such an interface contains just a single clone method.

The implementation of the clone method is very similar in all classes. The method creates an object of the current class and carries over all of the field values of the old object into the new one. You can even copy private fields because most programming languages let objects access private fields of other objects that belong to the same class.

An object that supports cloning is called a prototype. When your objects have dozens of fields and hundreds of possible configurations, cloning them might serve as an alternative to subclassing.

## Thoughts

Some suggest that the Prototype pattern is available in C# out of the box with a `ICloneable` interface. But remember that the Prototype pattern requires a deep copy to be performed and IClonable does not necessarily do that. Even framework classes like arrays implements IClonable but does a shallow copy. 

Also the `Clone` method returns and object type, so when you use the Clone method you must cast it explicitly.

```csharp

var john = new Person(new[] { "John", "Smith" }, new Address("Coventry Road", "London"));
Console.WriteLine(john);

// Need to cast to Person as ICloneable returns an object
var jane = (Person) john.Clone();
jane.Names[0] = "Jane";
Console.WriteLine(john);
Console.WriteLine(jane);


public class Person : ICloneable
{
    public string[] Names;
    private Address Address;

    public Person(string[] names, Address address)
    {
        Names = names;
        Address = address;
    }

    public object Clone()
    {
        // Event of the Address being a reference type, it will be copied by reference as its a shallow copy. 
        // could implement IClonable in the Address class but even the string[] class performs a shallow copy.
        return new Person((string[]) Names.Clone(), Address);
    }

    public override string ToString()
    {
        return $"{nameof(Names)}: {string.Join(" ", Names)}, {Address}";
    }
}

public class Address
{
    public string Street;
    public string City;

    public Address(string street, string city)
    {
        Street = street;
        City = city;
    }
    override public string ToString()
    {
        return $"{nameof(Address)}: {Street}, {City}";
    }
}
```

There are other implementations available for the prototype pattern but the recommended one (unless you are usina time critical system) is to implement an extension methon that uses Json serialize and deserialize.

```csharp 

public static class ExtensionMethods
{
    // Will generate SYSLIB0011: BinaryFormatter serialization is obsolete so must suppress warning.
    // Also all classes must have the [Serializable] attribute as well as enable it in the application by adding the following to the project file:
    // EnableUnsafeBinaryFormatterSerialization
    public static T DeepCopy<T>(this T self)
    {
        using (var stream = new MemoryStream())
        {
            #pragma warning disable SYSLIB0011
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, self);
            stream.Seek(0, SeekOrigin.Begin);
            object copy = formatter.Deserialize(stream);
            return (T)copy;
        }
    }

    // Note All classes must have a parameterless constructor
    public static T DeepCopyXml<T>(this T self)
    {
        using (var stream = new MemoryStream())
        {
            var s = new XmlSerializer(typeof(T));
            s.Serialize(stream, self);
            stream.Position = 0;
            return (T) s.Deserialize(stream);
        }
    }

    // Note Classes are not required to have a parameterless constructor
    public static T DeepCopyJson<T>(this T self) => JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(self));
}

```

