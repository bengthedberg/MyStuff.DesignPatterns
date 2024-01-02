// See https://aka.ms/new-console-template for more information

var shortDuration = Duration.DurationInSeconds(10);
Console.WriteLine(shortDuration);

var longDuration = Duration.DurationInMinutes(10);
Console.WriteLine(longDuration);


// This will not compile
//public class Duration
//{
//    public Duration(double seconds)
//    {
//    }
//    public Duration(double minutes)
//    {
//    }
//}


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

    override public string ToString()
        => $"{nameof(StartDate)}: {StartDate}, {nameof(EndDate)}: {EndDate}";
}