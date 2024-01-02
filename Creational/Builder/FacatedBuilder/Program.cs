// See https://aka.ms/new-console-template for more information

// This sample demonstrate how to combine multiple builders into a single one using the Facade pattern.

Person person = new PersonBuilder()
    .Works.At("Microsoft")
        .AsA("Software Engineer")
        .Earning(100000)
    .Lives.At("123 London Road")
        .In("London")
        .WithPostcode("SW12BC");
Console.WriteLine(person);

public class Person
{
    // address information
    public string StreetAddress { get; set; }  = string.Empty;
    public string Postcode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;

    // employment information
    public string Company { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public int AnnualIncome { get; set; }

    public override string ToString()
    {
        return $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(Postcode)}: {Postcode}, {nameof(City)}: {City}, {nameof(Company)}: {Company}, {nameof(Position)}: {Position}, {nameof(AnnualIncome)}: {AnnualIncome}";
    }
 
}

public class PersonBuilder // facade that combines all the builders
{
    // the object we're going to build
    protected Person person = new Person(); // this is a reference!
    public PersonJobBuilder Works => new PersonJobBuilder(person);
    public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

    public static implicit operator Person(PersonBuilder pb)
    {
        return pb.person;
    }
}

public class PersonAddressBuilder : PersonBuilder
{
    public PersonAddressBuilder(Person person) => this.person = person;
    public PersonAddressBuilder At(string streetAddress)
    {
        person.StreetAddress = streetAddress;
        return this;
    }
    public PersonAddressBuilder WithPostcode(string postcode)
    {
        person.Postcode = postcode;
        return this;
    }
    public PersonAddressBuilder In(string city)
    {
        person.City = city;
        return this;
    }
}   

public class PersonJobBuilder : PersonBuilder
{
    public PersonJobBuilder(Person person)
    {
        this.person = person;
    }
    public PersonJobBuilder At(string companyName)
    {
        person.Company = companyName;
        return this;
    }
    public PersonJobBuilder AsA(string position)
    {
        person.Position = position;
        return this;
    }
    public PersonJobBuilder Earning(int amount)
    {
        person.AnnualIncome = amount;
        return this;
    }
}