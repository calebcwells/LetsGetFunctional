namespace LetsGetFunctional;

public record Employee
{
    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public required IEnumerable<string> MiddleNames { get; init; }
}
