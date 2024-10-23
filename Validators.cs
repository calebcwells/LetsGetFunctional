namespace LetsGetFunctional;

public static class Validators
{
    public static bool IsInvalid<T>(this T @this, params Func<T, bool>[] rules) => !@this.IsValid(rules);

    public static bool IsPasswordValid(string password)
    {
        if(password.Length <= 6)
            return false;

        if(password.Length > 20)
            return false;

        if(!password.Any(x => char.IsLower(x)))
            return false;

        if(!password.Any(x => char.IsUpper(x)))
            return false;

        if(!password.Any(x => char.IsSymbol(x)))
            return false;

        if(password.Contains("Caleb", StringComparison.OrdinalIgnoreCase) &&
            password.Contains("Wells", StringComparison.OrdinalIgnoreCase))
            return false;

        return true;
    }

    public static bool IsPasswordValidFunc(string password) => password.IsValid(
        x => x.Length > 6,
        x => x.Length <= 20,
        x => x.Any(y => char.IsLower(y)),
        x => x.Any(y => char.IsUpper(y)),
        x => x.Any(y => char.IsSymbol(y)),
        x => !x.Contains("Caleb", StringComparison.OrdinalIgnoreCase) &&
            !x.Contains("Wells", StringComparison.OrdinalIgnoreCase));

    public static bool IsValid<T>(this T @this, params Func<T, bool>[] rules) => rules.All(x => x(@this));
}
