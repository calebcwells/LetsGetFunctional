using FluentAssertions;
using LetsGetFunctional.Extensions;
using LetsGetFunctional.Geometry;
using static System.Linq.Enumerable;
using static LetsGetFunctional.Tests.VatStrategy;

namespace LetsGetFunctional.Tests;

public class FunctionalTests
{
    [Fact]
    public void Separate_Base_And_Quote_From_String()
    {
        (string BaseCurrency, string QuoteCurrency) pair = "EURUSD".AsPair();
        pair.BaseCurrency.Should().Be("EUR");
        pair.QuoteCurrency.Should().Be("USD");
    }

    [Fact]
    public void Check_Circle_Area()
    {
        Circle circle = new(5);
        circle.Area().Should().Be(78.53981633974483);
    }

    [Fact]
    public void Check_Rectangle_Area()
    {
        Rectangle rectangle = new(4, 6);
        rectangle.Area().Should().Be(24);
    }

    [Fact]
    public void Return_Unsupported_Shape_Type()
    {
        IShape shape = null!;
        Action action = () => shape.Area();
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void First_Class_Function()
    {
        Func<int, int> triple = (int x) => x * 3;
        IEnumerable<int> range = Range(1, 3);
        IEnumerable<int> triples = range.Select(triple);
        
        triples.Should().BeEquivalentTo([3, 6, 9]);
    }

    [Fact]
    public void No_In_Place_Updates()
    {
        Func<int, bool> isOdd = (int x) => x % 2 == 1;
        int[] original = [7, 6, 1];

        IOrderedEnumerable<int> sorted = original.OrderBy(x => x);
        IEnumerable<int> filtered = original.Where(isOdd);
        
        original.Should().BeEquivalentTo([7, 6, 1]);
        sorted.Should().BeEquivalentTo([1, 6, 7]);
        filtered.Should().BeEquivalentTo([7, 1]);
    }

    [Fact]
    public void In_Place_Updates()
    {
        int[] original = [5, 7, 1];
        
        Array.Sort(original);
        
        original.Should().BeEquivalentTo([1, 5, 7]);
    }

    [Fact]
    public void Test_Vat_Strategy_By_Country()
    {
        Address address = new("it");

        decimal countryRate = RateByCountry(address.Country);
        
        countryRate.Should().Be(0.22m);
    }

    [Fact]
    public void Test_Vat_Strategy_Missing_Country()
    {
        Address address = new("au");
        
        Action act = () => RateByCountry(address.Country);
        
        act.Should().Throw<ArgumentException>().WithMessage($"Missing rate for {address.Country}");
    }

    [Fact]
    public void Return_Order_Price_Including_Vat()
    {
        Address address = new("jp");
        
        Order order = new(new Product("Product 1", 100), 1);
        
        decimal price = Vat(address, order);
        
        price.Should().Be(108m);
    }

    [Fact]
    public void Test_Country_With_Multiple_Vat_Rates()
    {
        Address address = new("de");
        
        Order regularOrder = new(new Product("Product 2", 50), 1);
        Order foodOrder = new(new Product("Product 3", 50, true), 1);
        
        decimal price = Vat(address, regularOrder);
        decimal foodPrice = Vat(address, foodOrder);
        
        price.Should().Be(60m);
        foodPrice.Should().Be(54m);
    }
}

public static class VatStrategy
{
    private static decimal CustomVat(Order order) =>
        order.NetPrice * (order.Product.IsFood ? 1 + 0.08m : 1 + 0.2m);

    private static decimal ProductPrice(decimal rate, Order order) => order.NetPrice * (1 + rate);

    public static decimal Vat(Address address, Order order) => address switch
    {
        (Country: "de") => CustomVat(order),
        (var country) _ => ProductPrice(RateByCountry(country), order)
    };

    public static decimal RateByCountry(string country) =>
        country switch
        {
            "it" => 0.22m,
            "jp" => 0.08m,
            _ => throw new ArgumentException($"Missing rate for {country}")
        };
}

public record Order(Product Product, int Quantity)
{
    public decimal NetPrice => Product.Price * Quantity;
}

public record Product(string Name, decimal Price, bool IsFood = false);

public record Address(string Country);
