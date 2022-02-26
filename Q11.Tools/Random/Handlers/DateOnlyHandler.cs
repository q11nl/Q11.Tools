namespace Q11.Tools.Random.Handlers;

internal class DateOnlyHandler : HandlerBase
{
    public DateOnlyHandler(GenericRandom genericRandom) : base(genericRandom)
    {
    }

    public override bool CanHandle<T>() => IsSameType<T, DateOnly>();

    public override T GetValue<T>(int level) =>
        new DateOnly(1970, 1, 1).AddDays(GetTnt(0, DaysIn60Years)).ConvertTo<DateOnly, T>();
}