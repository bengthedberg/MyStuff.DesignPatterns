// See https://aka.ms/new-console-template for more information

var person = new PersonBuilder()
    .Called("John")
    .WorksAsA("Accountant")
    .Build();
Console.WriteLine(person);

public class Person
{ 
    public string Name { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;

    override public string ToString()
        => $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
}

public abstract class FunctionalBuilder<TSubject, TSelf>
    where TSelf : FunctionalBuilder<TSubject, TSelf>
    where TSubject : new()
{
    private readonly List<Func<TSubject, TSubject>> actions = new List<Func<TSubject, TSubject>>();

    public TSelf Do(Action<TSubject> action)
        => AddAction(action);

    public TSubject Build()
        => actions.Aggregate(new TSubject(), (p, f) => f(p));

    private TSelf AddAction(Action<TSubject> action)
    {
        actions.Add(p =>
        {
            action(p);
            return p;
        });
        return (TSelf) this;
    }
}

public sealed class PersonBuilder : FunctionalBuilder<Person, PersonBuilder>
{
    public PersonBuilder Called(string name) 
       => Do(p => { p.Name = name; });
}


//public sealed class PersonBuilder
//{ 
//    private readonly List<Func<Person, Person>> actions = new List<Func<Person, Person>>();

//    public PersonBuilder Do(Action<Person> action) 
//        => AddAction(action);

//    public PersonBuilder Called(string name) 
//        => Do(p => { p.Name = name; });

//    public Person Build()
//        => actions.Aggregate(new Person(), (p, f) => f(p));

//    /// <summary>
//    /// Add a new action to the list of actions that turns into a function
//    /// </summary>
//    /// <param name="action"></param>
//    /// <returns></returns>
//    private PersonBuilder AddAction(Action<Person> action)
//    {
//        actions.Add(p =>
//        {
//            action(p);
//            return p;
//        });
//        return this;
//    }
//}



// Extension methods instead of inheritance to add new functionality (open-closed principle)
public static class PersonBuilderExtensions
{
    public static PersonBuilder WorksAsA
        (this PersonBuilder builder, string position)
        => builder.Do(p => { p.Position = position; });
}

