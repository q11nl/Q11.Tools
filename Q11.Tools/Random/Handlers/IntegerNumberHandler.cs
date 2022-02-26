namespace Q11.Tools.Random.Handlers;

internal class IntegerNumberHandler : HandlerBase
{
    public IntegerNumberHandler(GenericRandom genericRandom) : base(genericRandom)
    {
    }

    public override bool CanHandle<T>() => IsInTypes<T>(typeof(int), typeof(long));

    public override T GetValue<T>(int level) => GetTnt(10000000, 99999999).ConvertIConvertibleTo<int, T>();
}
