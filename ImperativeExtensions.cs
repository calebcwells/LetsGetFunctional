namespace LetsGetFunctional;

public static class ImperativeExtensions
{
    // Imperative Implementation of Pattern Matching
    public static TOutput Match<TInput, TOutput>(
        this TInput @this,
        params (Func<TInput, bool> IsMatch,
    Func<TInput, TOutput> Transform)[] matches)
    {
        (Func<TInput, bool> _, Func<TInput, TOutput> Transform) = matches.FirstOrDefault(x => x.IsMatch(@this));
        TOutput returnValue = Transform(@this) ?? default!;
        return returnValue;
    }
}
