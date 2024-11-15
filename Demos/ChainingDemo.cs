namespace LetsGetFunctional.Demos;

public class ChainingDemo : IDemoBase
{
    public void Run()
    {
        Box<int> numberHolder = new(25);
        Box<string> stringHolder = new("Twenty Five");

        Box<string> stringResult = numberHolder.Map(_ => "I have been transformed");

        // The Box is no longer valid after new Box<int>() so it will short-circuit and return an empty box
        Box<string> resultsFromChaining = stringHolder
            .Bind(s => new Box<int>(s.Length))
            .Bind(_ => new Box<int>())
            .Bind(_ => new Box<string>("I am back!"));

        Console.WriteLine($"The contents of my number holder is now '{stringResult.Item}'");
        Console.WriteLine($"The contents of my string holder is now '{resultsFromChaining.Item}'");
    }
}