// See https://aka.ms/new-console-template for more information


Console.WriteLine("Initialize Foo");
var x = await Foo.CreateAsync();
Console.WriteLine("Foo is initialized");

// This class ensure that all users will instantiate the class fully and in an asynchronous manner.
public class Foo
{
    // Make ctor private so no one can misuse the class as it needs to be initialized.
    private Foo() { }

    // Also make the initialize code private to prevent misuse.
    private async Task<Foo> InitAsync()
    {
        await Task.Delay(1000);
        return this;
    }

    // Asynchronous Factory Method
    // var x = await Foo.CreateAsync();
    public static Task<Foo> CreateAsync()
    {
        var result = new Foo();
        return result.InitAsync();
    }
}