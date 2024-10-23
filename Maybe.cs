using System;
using System.Linq;

namespace LetsGetFunctional;

public abstract class Maybe<T>
{
}

public class Something<T> : Maybe<T>
{
    public Something(T value) { Value = value; }

    public T Value { get; init; }
}

public class Nothing<T> : Maybe<T>
{
}

//public abstract class Result<T>
//{
//}

//public class Success : Result<T>
//{
//    public Success<T>(T value)
//    {
//     this.Value = value;
//    }

//public T Value { get; init; }
//}

public class Failure<T> : Maybe<T>
{
    public Failure(Exception e) { Error = e; }

    public Exception Error { get; init; }
}

public abstract class Either<T1, T2>
{
}

public class Left<T1, T2> : Either<T1, T2>
{
    public Left(T1 value) { Value = value; }

    public T1 Value { get; init; }
}

public class Right<T1, T2> : Either<T1, T2>
{
    public Right(T2 value) { Value = value; }

    public T2 Value { get; init; }
}

public class State<TS, TV>
{
    public State(TS s, TV v)
    {
        CurrentValue = v;
        CurrentState = s;
    }

    public TS CurrentState { get; init; }

    public TV CurrentValue { get; init; }
}