using LetsGetFunctional.Infrastructure;
using LetsGetFunctional.Monads;

namespace LetsGetFunctional.Demos;

public class TransformationsDemo : IDemoBase
{
    public void Run()
    {
        Box<int[]> numbersMap = new([11, 12, 13, 14, 15, 16, 17, 18, 19, 20]);
        Box<int[]> numbersBind = new([1, 2, 3, 4, 5, 6, 7, 8, 9, 10]);

        Box<int[]> mappedResult = numbersMap.Map(MapFunction);
        Box<int[]> boundResult = numbersBind.Bind(BindFunction);

        Console.WriteLine(string.Join(", ", mappedResult.Item));
        Console.WriteLine(string.Join(", ", boundResult.Item));
    }

    private static int[] MapFunction(int[] numbers) => numbers.Select(x => x + 2).ToArray();
    private static Box<int[]> BindFunction(int[] numbers) => new(numbers.Select(x => x + 1).ToArray());
}