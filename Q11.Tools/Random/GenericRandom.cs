using Q11.Tools.Random.Handlers;

namespace Q11.Tools.Random;

public class GenericRandom
{
    public const int MaximumLevelsDeep = 2;
    internal System.Random Random { get; }

    private readonly IReadOnlyList<HandlerBase> _handlers;

    public GenericRandom(): this(new System.Random())
    {
    }

    public GenericRandom(int seed): this(new System.Random(seed))
    {
    }

    private GenericRandom(System.Random random)
    {
        Random = random;
        _handlers = GetHandlers();
    }

    private IReadOnlyList<HandlerBase> GetHandlers()
    {
        return new HandlerBase[]
        {
            new StringHandler(this),
            new IntegerNumberHandler(this),
            new DecimalNumberHandler(this),
            new ByteHandler(this),
            new SByteHandler(this),
            new GuidHandler(this),
            new CharHandler(this),
            new DateTimeHandler(this),
            new DateOnlyHandler(this),
            new TimeOnlyHandler(this),
            new TimeSpanHandler(this),
            new ListHandler(this),
            new ClassHandler(this),
        };
    }

    public T Next<T>() => NextWithLevel<T>(0)!;


    internal T NextWithLevel<T>(int level)
    {
        var handler = _handlers.FirstOrDefault(h => h.CanHandle<T>());
        if (handler is { })
        {
            var result =  handler.GetValue<T>(level);
            //if (result is null)
            //{
            //    throw new NullReferenceException($"A handler {handler.GetType().Name} returns a null value.");
            //}
            return result;
        }

        var type = typeof(T);
        throw new NotSupportedException(
            $"{type} is not supported. Use the {nameof(NextOrDefaultWithLevel)} method if you want to return the default value in this case.");
    }

    public T? NextOrDefault<T>()
    {
        try
        {
            return Next<T>();
        }
        catch (Exception)
        {
            return default;
        }
    }

    internal T? NextOrDefaultWithLevel<T>(int level)
    {
        try
        {
            return NextWithLevel<T>(level);
        }
        catch (Exception)
        {
            return default;
        }
    }
}