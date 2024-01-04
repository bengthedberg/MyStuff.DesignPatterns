using System;

SingletonLogger.Instance.Log("First Message");
SingletonLogger.Instance.Log("Second Message");

public class SingletonLogger
{
    private static int instanceCount;

    private static readonly Lazy<SingletonLogger> lazyInstance =
        new Lazy<SingletonLogger>(() => new SingletonLogger());

    private SingletonLogger() 
    {
        instanceCount++;
        Console.WriteLine($"Instances: {instanceCount}");
    }

    public static SingletonLogger Instance => lazyInstance.Value;

    public void Log(string message)
    {
        Console.WriteLine(message);
    }
}