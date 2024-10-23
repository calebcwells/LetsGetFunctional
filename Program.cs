using LetsGetFunctional.Monads;

// Monads
int theAnswer = 42;
// Implicit conversion of int instead of using new()
Box<int> box = theAnswer;
Console.WriteLine($"The contents of my box is initially is '{box.Item}'");

box = box.Select(x => x + 1);
Console.WriteLine($"The contents of my box is now '{box.Item}'");


// Transformations
Box<int[]> numbersMap = new([11, 12, 13, 14, 15, 16, 17, 18, 19, 20]);
Box<int[]> numbersBind = new([1, 2, 3, 4, 5, 6, 7, 8, 9, 10]);

Box<int[]> mappedResult = numbersMap.Map(MapFunction);
Box<int[]> boundResult = numbersBind.Bind(BindFunction);

Console.WriteLine(string.Join(", ", mappedResult.Item));
Console.WriteLine(string.Join(", ", boundResult.Item));

static int[] MapFunction(int[] numbers) => numbers.Select(x => x + 2).ToArray();
static Box<int[]> BindFunction(int[] numbers) => new(numbers.Select(x => x + 1).ToArray());


// Chaining/Pipelines
Box<int> numberHolder = new(25);
Box<string> stringHolder = new("Twenty Five");

Box<string> stringResult = numberHolder.Map(i => "I have been transformed");

// The Box is no longer valid after new Box<int>() so it will short-circuit and return an empty box
Box<string> resultsFromChaining = stringHolder
    .Bind(s => new Box<int>(s.Length))
    .Bind(i => new Box<int>())
    .Bind(i => new Box<string>("I am back!"));

Console.WriteLine($"The contents of my number holder is now '{stringResult.Item}'");
Console.WriteLine($"The contents of my string holder is now '{resultsFromChaining.Item}'");


// Adding SelectMany
Box<int[]> boxOfNumbers = new([1, 2, 3, 4, 5, 6, 7, 8, 9, 10]);

Box<int[]> doubleBind = DoubleBoxBind(boxOfNumbers);
Box<int[]> doubleMap = DoubleBoxMap(boxOfNumbers);
Box<int[]> doubleMany = DoubleBoxMany(boxOfNumbers);
Box<int[]> addMany = AddBoxMany(boxOfNumbers);

Console.WriteLine(string.Join(", ", doubleBind.Item));
Console.WriteLine(string.Join(", ", doubleMap.Item));
Console.WriteLine(string.Join(", ", doubleMany.Item));
Console.WriteLine(string.Join(", ", addMany.Item));

static Box<int[]> DoubleBoxBind(Box<int[]> boxOfNumbers) => boxOfNumbers.Bind(DoubleNumbers);
static Box<int[]> DoubleBoxMap(Box<int[]> boxOfNumbers) => boxOfNumbers.Map(DoubleNumbersNoBox);
static Box<int[]> DoubleBoxMany(Box<int[]> boxOfNumbers) => boxOfNumbers.SelectMany(
    DoubleNumbers,
    (original, doubled) => doubled);
static Box<int[]> AddBoxMany(Box<int[]> boxOfNumbers) => boxOfNumbers.SelectMany(
    AddNumbers,
    (original, combined) => combined);

static Box<int[]> DoubleNumbers(int[] extract) => new(extract.Select(x => x * 2).ToArray());
static int[] DoubleNumbersNoBox(int[] extract) => extract.Select(x => x * 2).ToArray();
static Box<int[]> AddNumbers(int[] add) => add.Concat([11, 12, 13, 14, 15]).ToArray();


Console.ReadLine();