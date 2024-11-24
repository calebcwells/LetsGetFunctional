namespace LetsGetFunctional.Demos;

public class MonadsDemo : IDemoBase
{
    public void Run()
    {
        const int theAnswer = 42;

        // Implicit conversion of int instead of using new()
        Box<int> box = theAnswer;
        Console.WriteLine($"The contents of my box is initially is '{box.Item}'");

        box = box.Select(x => x + 1);
        Console.WriteLine($"The contents of my box is now '{box.Item}'");
    }
}