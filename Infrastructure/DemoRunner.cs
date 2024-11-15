namespace LetsGetFunctional.Infrastructure;

public static class DemoRunner
{
    public static void Run<T>() where T : IDemoBase, new()
    {
        var demoInstance = new T();
        demoInstance.Run();
    }
}