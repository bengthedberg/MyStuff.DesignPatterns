// See https://aka.ms/new-console-template for more information
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using Newtonsoft.Json;

var john = new Person(new[] { "John", "Smith" }, new Address("Coventry Road", "London"));
Console.WriteLine(john);

// Need to cast to Person as ICloneable returns an object
//var jane = john.DeepCopyXml();
var jane = john.DeepCopyJson();
jane.Names[0] = "Jane";
jane.Address.Street = "Baker Street";
Console.WriteLine(john);
Console.WriteLine(jane);

//var jill = jane.DeepCopy();
//jill.Names[0] = "Jill";
//jill.Address.Street = "Oxford Street";
//Console.WriteLine(jane);
//Console.WriteLine(jill);

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

public class Person 
{
    public string[] Names;
    public Address Address;

    public Person(string[] names, Address address)
    {
        Names = names;
        Address = address;
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

