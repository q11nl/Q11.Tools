namespace Q11.Tools.Random.Handlers;

internal class StringHandler: HandlerBase
{
    public StringHandler(GenericRandom genericRandom) : base(genericRandom)
    {
    }

    public override bool CanHandle<T>() => IsSameType<T, string>();

    public override T GetValue<T>(int level)
    {
        const int length = 8;
        var randomChars = Enumerable.Repeat(1, length)
            .Select(_ => GetChar())
            .ToArray();
        return new string(randomChars).ConvertIConvertibleTo<string, T>();
    }
}