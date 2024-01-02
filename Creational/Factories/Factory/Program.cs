// See https://aka.ms/new-console-template for more information

var cartesianPoint = PointFactory.NewCartesianPoint(1, 2);
var polarPoint = PointFactory.NewPolarPoint(1, 2);
Console.WriteLine(cartesianPoint.DistanceTo(polarPoint));

var anotherCartesianPoint = PointFactory.NewCartesianPoint(1, 3);
Console.WriteLine(cartesianPoint.DistanceTo(anotherCartesianPoint));

public record Point
{
    public readonly double x, y;
    internal Point(double x, double y)
    {
        this.x = x;
        this.y = y;
    }
}

public static class PointFactory
{
    public static Point NewCartesianPoint(double x, double y)
        => new Point(x, y);
    public static Point NewPolarPoint(double rho, double theta)
        => new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
}


public static class PointExtensions
{
    public static double DistanceTo(this Point point, Point otherPoint)
    {
        var a = point.x - otherPoint.x;
        var b = point.y - otherPoint.y;
        return Math.Sqrt(a * a + b * b);
    }
}