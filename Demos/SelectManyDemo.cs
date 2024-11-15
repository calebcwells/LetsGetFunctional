namespace LetsGetFunctional.Demos;

public class SelectManyDemo : IDemoBase
{
    public void Run()
    {
        Box<int[]> boxOfNumbers = new([1, 2, 3, 4, 5, 6, 7, 8, 9, 10]);

        Box<int[]> doubleBind = DoubleBoxBind(boxOfNumbers);
        Box<int[]> doubleMap = DoubleBoxMap(boxOfNumbers);
        Box<int[]> doubleMany = DoubleBoxMany(boxOfNumbers);
        Box<int[]> addMany = AddBoxMany(boxOfNumbers);

        Box<int[]> doubledUp = boxOfNumbers
            .SelectMany(AddNumbers, (extract, transformed) => (extract, transformed))
            .SelectMany(t => DoubleNumbers(t.transformed), (t, transformed2) => (t, transformed2))
            .SelectMany(t => AddNumbers(t.transformed2), (t, transformed3) => (t, transformed3))
            .SelectMany(t => DoubleNumbers(t.transformed3), (t, transformed4) => (t, transformed4))
            .Select(t => t.transformed4);

        Console.WriteLine(string.Join(", ", doubleBind.Item));
        Console.WriteLine(string.Join(", ", doubleMap.Item));
        Console.WriteLine(string.Join(", ", doubleMany.Item));
        Console.WriteLine(string.Join(", ", addMany.Item));
        Console.WriteLine(string.Join(", ", doubledUp.Item));
    }

    private static Box<int[]> DoubleBoxBind(Box<int[]> boxOfNumbers) => boxOfNumbers.Bind(DoubleNumbers);
    private static Box<int[]> DoubleBoxMap(Box<int[]> boxOfNumbers) => boxOfNumbers.Map(DoubleNumbersNoBox);

    private static Box<int[]> DoubleBoxMany(Box<int[]> boxOfNumbers) => boxOfNumbers.SelectMany(
        DoubleNumbers,
        (_, doubled) => doubled);

    private static Box<int[]> AddBoxMany(Box<int[]> boxOfNumbers) => boxOfNumbers.SelectMany(
        AddNumbers,
        (_, combined) => combined);

    private static Box<int[]> DoubleNumbers(int[] extract) => new(extract.Select(x => x * 2).ToArray());
    private static int[] DoubleNumbersNoBox(int[] extract) => extract.Select(x => x * 2).ToArray();
    private static Box<int[]> AddNumbers(int[] add) => add.Concat([11, 12, 13, 14, 15]).ToArray();
}