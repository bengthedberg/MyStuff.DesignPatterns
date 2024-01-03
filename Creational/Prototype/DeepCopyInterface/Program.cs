// See https://aka.ms/new-console-template for more information
var john = new Person(new[] { "John", "Smith" }, new Address("Coventry Road", "London"));
Console.WriteLine(john);

// Need to cast to Person as ICloneable returns an object
var jane = john.DeepCopy();
jane.Names[0] = "Jane";
jane.Address.Street = "Baker Street";
Console.WriteLine(john);
Console.WriteLine(jane);

// Deep copy interface
public interface IPrototype<T>
{
    T DeepCopy();
}

public class Person : IPrototype<Person>
{
    public string[] Names;
    public Address Address;

    public Person(string[] names, Address address)
    {
        Names = names;
        Address = address;
    }

    // still have to iterate through all the properties and copy them if they are reference types
    public Person DeepCopy()
    {
        return new Person((string[]) Names.Clone(), Address.DeepCopy());
    }

    public override string ToString()
    {
        return $"{nameof(Names)}: {string.Join(" ", Names)}, {Address}";
    }
}

public class Address : IPrototype<Address>
{
    public string Street;
    public string City;

    public Address(string street, string city)
    {
        Street = street;
        City = city;
    }

    public Address DeepCopy()
    {
        return new Address(Street, City);
    }

    override public string ToString()
    {
        return $"{nameof(Address)}: {Street}, {City}";
    }
}

