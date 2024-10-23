
namespace LetsGetFunctional.Monads;

public class Box<T>
{
    private T _item = default!;

    public Box()
    {
    }

    public Box(T newItem)
    {
        Item = newItem;
        IsEmpty = false;
    }

    public static implicit operator Box<T>(T value) => new(value) { IsEmpty = false };

    public bool IsEmpty { get; private set; } = true;

    public T Item
    {
        get => _item;
        set
        {
            _item = value;
            IsEmpty = false;
        }
    }
}

public static class BoxMethods
{
    public static Box<TOut> Select<TIn, TOut>(this Box<TIn> box, Func<TIn, TOut> map) => box.IsEmpty
        ? new Box<TOut>()
        : new Box<TOut>(map(box.Item));

    public static Box<TB> Bind<TA, TB>(this Box<TA> box, Func<TA, Box<TB>> bind) => box.IsEmpty
        ? new Box<TB>()
        : bind(box.Item);

    public static Box<TOut> Map<TIn, TOut>(this Box<TIn> box, Func<TIn, TOut> select) => box.IsEmpty
        ? new Box<TOut>()
        : new Box<TOut>(select(box.Item));

    public static Box<TOut> SelectMany<TIn, TBind, TOut>(this Box<TIn> box, Func<TIn, Box<TBind>> bind, Func<TIn, TBind, TOut> project) => box.IsEmpty
        ? new Box<TOut>()
        : bind(box.Item) switch
        {
            { IsEmpty: true } => new Box<TOut>(),
            Box<TBind> liftedResult => new Box<TOut>(project(box.Item, liftedResult.Item))
        };
}