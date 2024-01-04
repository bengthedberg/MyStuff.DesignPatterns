using System;

var t1 = Task.Factory.StartNew(() =>
{
    PerThreadSingletonLogger.Instance.Log($"First Message thread {PerThreadSingletonLogger.Instance.Id}");
});
var t2 = Task.Factory.StartNew(() =>
{
    // Not the same instance as the above thread
    PerThreadSingletonLogger.Instance.Log($"Second Message in thread {PerThreadSingletonLogger.Instance.Id}");
    // Make sure the instance is reused
    PerThreadSingletonLogger.Instance.Log($"Third Message in thread {PerThreadSingletonLogger.Instance.Id}");
});

Task.WaitAll(t1, t2);


public class PerThreadSingletonLogger
{
    private int id;
    public int Id { get { return id; } }

    private static readonly ThreadLocal<PerThreadSingletonLogger> lazyInstance =
        new ThreadLocal<PerThreadSingletonLogger>(() => new PerThreadSingletonLogger());

    private PerThreadSingletonLogger()
    {
        id = Thread.CurrentThread.ManagedThreadId;
        Console.WriteLine($"Instance created with thread {id}");
    }

    public static PerThreadSingletonLogger Instance => lazyInstance.Value ?? throw new NullReferenceException($"{nameof(PerThreadSingletonLogger)}");

    public void Log(string message)
    {
        Console.WriteLine(message);
    }
}