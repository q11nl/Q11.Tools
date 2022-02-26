namespace Q11.Tools.Random.Handlers;

internal class ByteHandler : HandlerBase
{
    public ByteHandler(GenericRandom genericRandom) : base(genericRandom)
    {
    }

    public override bool CanHandle<T>() => IsSameType<T, byte>();

    public override T GetValue<T>(int level) => GetTnt(0, 255).ConvertIConvertibleTo<int, T>();
}