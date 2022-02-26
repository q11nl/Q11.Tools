namespace Q11.Tools.Random.Handlers;

internal class DecimalNumberHandler : HandlerBase
{
    public DecimalNumberHandler(GenericRandom genericRandom) : base(genericRandom)
    {
    }

    public override bool CanHandle<T>() => IsInTypes<T>(typeof(decimal), typeof(double), typeof(float));

    public override T GetValue<T>(int level) => GetDecimal(100000, 999999).ConvertIConvertibleTo<decimal, T>();
}