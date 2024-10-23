using LetsGetFunctional;

int[] numbers = [4, 8, 15, 16, 23, 42];

var transformer = (IEnumerable<int> x) => x
    .Select(y => y + 5)
    .Select(y => y * 10)
    .Where(y => y > 100);

var aggregator = (IEnumerable<int> x) => string.Join(", ", x);

string transduceOutput = numbers.Transduce(transformer, aggregator);

string[] liberatorCrew = ["Roj Blake", "Kerr Avon", "Vila Restal", "Jenna Stannis", "Cally", "Olag Gan", "Zen"];
IEnumerable<string> filteredList = liberatorCrew.Where(x => x.First() > 'M');

var formatDecimal = (decimal x) => x
    .Map(x => Math.Round(x, 2))
    .Map(x => $"{x}°F");

string FahrenheitToCelsius(decimal tempInF) => tempInF.Map(x => x - 32)
    .Map(x => x * 5)
    .Map(x => x / 9)
    .Tap(x => Console.WriteLine($"The value before rounding is {x}"))
    .Map(x => Math.Round(x, 2))
    .Map(x => $"{x}°C");

var celsiusToFahrenheit = (decimal x) => x.Map(x => x * 9).Map(x => x / 5).Map(x => x + 32);

Func<decimal, string> composedCtoF = celsiusToFahrenheit.Compose(formatDecimal);

string CtoFOutput = composedCtoF(37.78M);

static Func<int, int> MakeAddFunc(int x) => y => x + y;

Func<int, int> addTenFunction = MakeAddFunc(10);
int answer = addTenFunction(5);

int average = numbers.Fork(
    x => x.Sum(),
    x => x.Count(),
    (s, c) => s / c
);

string temperature = FahrenheitToCelsius(100);

string[] sourceData = ["Hello", "Doctor", "Yesterday", "Today", "Tomorrow", "Continue"];

IEnumerable<string> updatedData = sourceData.ReplaceAt(1, "Darkness, my old friend");
string finalString = string.Join(" ", updatedData);

Employee caleb = new() { FirstName = "Caleb", LastName = "Wells", MiddleNames = ["Craig", "Larry"] };

Func<int, int> MultiplyByTwo = x => x * 2;
int input = 100;
int runFunctionOutput = MultiplyByTwo(input);

Maybe<int> monadOutput = new Something<int>(input).Bind(MultiplyByTwo);

var Add = (decimal x, decimal y) => x + y;
Func<decimal, Func<decimal, decimal>> CurriedAdd = Add.Curry();

Func<decimal, decimal> add10 = CurriedAdd(10);
decimal curriedAnswer = add10(100);

Console.WriteLine(curriedAnswer);

Console.WriteLine(runFunctionOutput);

Console.WriteLine(monadOutput);

Console.WriteLine(finalString);

Console.WriteLine(transduceOutput);

Console.WriteLine(average);

Console.WriteLine(CtoFOutput);

Console.WriteLine(temperature);

Console.WriteLine(answer);

Console.WriteLine(filteredList);

Console.WriteLine(Descriptors.Describe(caleb));

Console.WriteLine(Validators.IsPasswordValid("CalebWells"));
