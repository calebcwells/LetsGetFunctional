namespace LetsGetFunctional.Geometry;

public static class Geometry
{
    public static double Area(this IShape shape) =>
        shape switch
        {
            Rectangle(var l, var h) => l * h,
            Circle(var r) => Math.PI * Math.Pow(r, 2),
            _ => throw new ArgumentException("Unsupported shape type")
        };
}