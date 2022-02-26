namespace Q11.Tools.Random.Handlers;

internal class SByteHandler : HandlerBase
{
    public SByteHandler(GenericRandom genericRandom) : base(genericRandom)
    {
    }

    public override bool CanHandle<T>() => IsSameType<T, sbyte>();

    public override T GetValue<T>(int level) => GetTnt(-128, 127).ConvertIConvertibleTo<int, T>();
}