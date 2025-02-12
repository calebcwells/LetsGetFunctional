namespace LetsGetFunctional.Extensions;

public static class StringExtensions
{
    public static (string BaseCurrency, string QuoteCurrency) AsPair(this string pairString) =>
        pairString.SplitAt(3);
    
    private static (string Left, string Right) SplitAt(this string str, int index) =>
        (str[..index], str[index..]);
}