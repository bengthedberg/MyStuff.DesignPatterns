// See https://aka.ms/new-console-template for more information

// How do we use the fluent builder pattern when the order of the methods are important?

// In this case, the car type must be set first as the valid wheel size depends on the car type. So we can call the WithWheel method until the CarType is set.

// This is an example of how to specify the order of the builder methods using separate interfaces:

var car = CarBuilder.Create()  // returns ISpecifyCarType
    .OfType(CarType.SUV)       // returns ISpecifyWheelSize
    .WithWeels(20)             // returns IBuildCar
    .Build();
Console.WriteLine(car);

public interface ISpecifyCarType
{ 
    ISpecifyWheelSize OfType(CarType type);
}

public interface ISpecifyWheelSize
{
    IBuildCar WithWeels(int size);
}

public interface IBuildCar
{
    Car Build();
}


public class CarBuilder
{
    // Note that we can not implement the interfaces directly in the CarrBuilder class as the order of the methods of each interface is important.
    private class Impl : ISpecifyCarType, ISpecifyWheelSize, IBuildCar
    {
        private Car _car = new Car();
        
        public ISpecifyWheelSize OfType(CarType type)
        {
            _car.Type = type;
            return this;
        }
        public IBuildCar WithWeels(int size)
        {
            switch (_car.Type)
            {
                case CarType.SUV when size < 17 || size > 20 :                    
                case CarType.Sedan when size < 15 || size > 17:                    
                    throw new ArgumentException("Invalid wheel size");
            }
            _car.WheelSize = size;
            return this;
        }
        public Car Build()
        {
            return _car;
        }
    }

    public static ISpecifyCarType Create()
    {
        return new Impl();
    }

}

public enum CarType
{
    Sedan,
    SUV
}

public class Car
{
    public CarType Type { get; set; }
    public int WheelSize { get; set; }
    override public string ToString()
    {
        return $"Car type: {Type}, wheel size: {WheelSize}";
    }
}
