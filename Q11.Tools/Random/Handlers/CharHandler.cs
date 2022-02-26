namespace Q11.Tools.Random.Handlers;

internal class CharHandler : HandlerBase
{
    public CharHandler(GenericRandom genericRandom) : base(genericRandom)
    {
    }

    public override bool CanHandle<T>() => IsSameType<T, char>();

    public override T GetValue<T>(int level) => GetChar().ConvertIConvertibleTo<char, T>();
}