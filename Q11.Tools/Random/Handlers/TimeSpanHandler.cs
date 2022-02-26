namespace Q11.Tools.Random.Handlers;

internal class TimeSpanHandler : HandlerBase
{
    public TimeSpanHandler(GenericRandom genericRandom) : base(genericRandom)
    {
    }

    public override bool CanHandle<T>() => IsSameType<T, TimeSpan>();

    public override T GetValue<T>(int level) =>
        new TimeSpan(GetTnt(1, 10), GetTnt(0, 23), GetTnt(0, 60), GetTnt(0, 60), GetTnt(0, 999)).ConvertTo<TimeSpan, T>();
}