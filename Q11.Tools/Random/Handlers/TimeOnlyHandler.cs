namespace Q11.Tools.Random.Handlers;

internal class TimeOnlyHandler : HandlerBase
{
    public TimeOnlyHandler(GenericRandom genericRandom) : base(genericRandom)
    {
    }

    public override bool CanHandle<T>() => IsSameType<T, TimeOnly>();

    public override T GetValue<T>(int level) =>
        new TimeOnly(GetTnt(0, 23), GetTnt(0, 59), GetTnt(0, 59)).ConvertTo<TimeOnly, T>();
}