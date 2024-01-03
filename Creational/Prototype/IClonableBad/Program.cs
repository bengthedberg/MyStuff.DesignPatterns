// See https://aka.ms/new-console-template for more information
var john = new Person(new[] { "John", "Smith" }, new Address("Coventry Road", "London"));
Console.WriteLine(john);

// Need to cast to Person as ICloneable returns an object
var jane = (Person) john.Clone();
jane.Names[0] = "Jane";
jane.Address.Street = "Baker Street";
Console.WriteLine(john);
Console.WriteLine(jane);


public class Person : ICloneable
{
    public string[] Names;
    public Address Address;

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

