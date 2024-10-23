namespace LetsGetFunctional;

public static class Descriptors
{
    private static readonly IEnumerable<Func<Employee, string>> descriptors =
    [
        d => $"First Name = {d.FirstName}",
        d => $"Last Name = {d.LastName}",
        d => d.MiddleNames.Any() ? $"Middle Name(s) = {string.Join(" ", d.MiddleNames)}" : string.Empty 
    ];

    public static string Describe(Employee employee) => string.Join(
        Environment.NewLine,
        descriptors.Select(d => d(employee)));
}
