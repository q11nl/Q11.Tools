using Q11.Tools.Conversion.Handlers.Base;
using Q11.Tools.Conversion.Pocos;

namespace Q11.Tools.Conversion.Handlers;

internal class TimeSpanToTimeOnlyHandler : Handler<TimeSpan, TimeOnly>
{
    public override T? GetValue<T>(ChangeTypeRequest<T> request) where T : default
    {
        var timeSpan = (TimeSpan)request.value;
        var timeSpanExcludingDays = timeSpan.Add(new TimeSpan(timeSpan.Days, 0, 0, 0).Negate());
        return (T)(object)new TimeOnly(timeSpanExcludingDays.Ticks);
    }
}