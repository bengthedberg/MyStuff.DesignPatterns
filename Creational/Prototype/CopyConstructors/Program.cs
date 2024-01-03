var john = new Person(new[] { "John", "Smith" }, new Address("Coventry Road", "London"));
Console.WriteLine(john);

// Call the copy constructor
var jane = new Person(john);
jane.Names[0] = "Jane";
jane.Address.Street = "Baker Street";
Console.WriteLine(john);
Console.WriteLine(jane);

public class Person 
{
    public string[] Names;
    public Address Address;

    public Person(string[] names, Address address)
    {
        Names = names;
        Address = address;
    }

    // Copy constructor
    public Person(Person other)
    {
        Names = other.Names;
        Address = new Address(other.Address ?? throw new ArgumentNullException($"{nameof(Address)}"));
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

    // Copy constructor
    public Address(Address other)
    {
        Street = other.Street;
        City = other.City;
    }

    public override string ToString()
    {
        return $"{nameof(Address)}: {Street}, {City}";
    }
}

