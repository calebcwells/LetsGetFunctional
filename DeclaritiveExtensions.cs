namespace LetsGetFunctional
{
    public static class DeclarativeExtensions
    {
        public static TOut Alt<TIn, TOut>(this TIn @this, params Func<TIn, TOut>[] args) => args.Select(x => x(@this))
            .First(x => x is not null);

        public static Maybe<TOut> Bind<TIn, TOut>(this Maybe<TIn> @this, Func<TIn, TOut> f)
        {
            try
            {
                Maybe<TOut> updatedValue = @this switch
                {
                    Something<TIn> s when !EqualityComparer<TIn>.Default.Equals(s.Value, default) => new Something<TOut>(
                        f(s.Value)),
                    Something<TIn> _ => new Nothing<TOut>(),
                    Nothing<TIn> _ => new Nothing<TOut>(),
                    Failure<TIn> e => new Failure<TOut>(e.Error),
                    _ => new Failure<TOut>(new Exception($"New Maybe state that isn't coded for!: {@this.GetType()}"))
                };
                return updatedValue;
            } catch(Exception e)
            {
                return new Failure<TOut>(e);
            }
        }

        //public static State<TS, TNew> Bind<TS, TOld, TNew>(this State<TS, TOld> @this, Func<TS, TOld, TNew> f) => new(
        //    @this.CurrentState,
        //    @this.CurrentValue.Bind(x => f(@this.CurrentState, x)));

        public static Func<TIn, NewTOut> Compose<TIn, OldTOut, NewTOut>(
            this Func<TIn, OldTOut> @this,
            Func<OldTOut, NewTOut> f) => x => f(@this(x));

        public static Func<T1, Func<T2, T3>> Curry<T1, T2, T3>(this Func<T1, T2, T3> @this) => (T1 x) => (T2 y) => @this(
            x,
            y);

        public static Func<T1, Func<T2, Func<T3, T4>>> Curry<T1, T2, T3, T4>(this Func<T1, T2, T3, T4> @this) => (T1 x) => (
            T2 y) => (T3 z) => @this(x, y, z);

        public static Func<T1, Func<T2, Func<T3, Func<T4, T5>>>> Curry<T1, T2, T3, T4, T5>(
            this Func<T1, T2, T3, T4, T5> @this) => (T1 x) => (T2 y) => (T3 z) => (T4 a) => @this(x, y, z, a);

        public static TOut Fork<TIn, T1, T2, TOut>(
            this TIn @this,
            Func<TIn, T1> f1,
            Func<TIn, T2> f2,
            Func<T1, T2, TOut> fout)
        {
            T1? p1 = f1(@this);
            T2? p2 = f2(@this);
            TOut? result = fout(p1, p2);
            return result;
        }

        public static TOut Map<TIn, TOut>(this TIn @this, Func<TIn, TOut> f) => f(@this);

        public static IEnumerable<T> ReplaceAt<T>(this IEnumerable<T> @this, int loc, T replacement) => @this.Select(
            (x, i) => i == loc ? replacement : x);

        public static T Tap<T>(this T @this, Action<T> action)
        {
            action(@this);
            return @this;
        }

        //public static async Task<Maybe<TOut>> BindAsync<TIn, TOut>(
        // this Maybe<TIn> @this,
        // Func<TIn, Task<TOut>> f)
        //{
        //    try
        //    {
        //        Maybe<TOut> updatedValue = @this switch
        //        {
        //            Something<TIn> s when
        //                EqualityComparer<TIn>.Default.Equals(s.Value, default) =>
        //                    new Something<TOut>(await f(s.Value)),
        //            Something<TIn> _ => new Nothing<TOut>(),
        //            Nothing<TIn> _ => new Nothing<TOut>(),
        //            Error<TIn> e => new Error<TOut>(e.ErrorMessage),
        //            _ => new Error<TOut>(
        //                new Exception("New Maybe state that isn't coded for!: " +
        //                    @this.GetType()))
        //        };
        //        return updatedValue;
        //    }
        //    catch (Exception e)
        //    {
        //        return new Error<TOut>(e);
        //    }
        //}
        public static State<TS, TV> ToState<TS, TV>(this TS @this, TV value) => new(@this, value);

        public static TFinalOut Transduce<TIn, TFilterOut, TFinalOut>(
            this IEnumerable<TIn> @this,
            Func<IEnumerable<TIn>, IEnumerable<TFilterOut>> transformer,
            Func<IEnumerable<TFilterOut>, TFinalOut> aggregator) => aggregator(transformer(@this));

        public static void Unless<T>(this T @this, Func<T, bool> condition, Action<T> f)
        {
            if(!condition(@this))
                f(@this);
        }

        public static State<TS, TV> Update<TS, TV>(this State<TS, TV> @this, Func<TS, TS> f) => new(
            f(@this.CurrentState),
            @this.CurrentValue);
    }
}