namespace Q11.Tools.Random.Handlers;

internal class DateTimeHandler : HandlerBase
{
    public DateTimeHandler(GenericRandom genericRandom) : base(genericRandom)
    {
    }

    public override bool CanHandle<T>() => IsSameType<T, DateTime>();

    public override T GetValue<T>(int level) => new DateTime(1970, 1, 1).AddDays(GetTnt(0, DaysIn60Years))
        .AddHours(GetTnt(0, 23))
        .AddMinutes(GetTnt(0, 59))
        .AddSeconds(GetTnt(0, 59)).ConvertIConvertibleTo<DateTime, T>();

}