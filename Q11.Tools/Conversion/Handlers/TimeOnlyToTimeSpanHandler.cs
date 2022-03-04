using Q11.Tools.Conversion.Handlers.Base;
using Q11.Tools.Conversion.Pocos;

namespace Q11.Tools.Conversion.Handlers;

internal class TimeOnlyToTimeSpanHandler : Handler<TimeOnly, TimeSpan>
{
    public override T? GetValue<T>(ChangeTypeRequest<T> request) where T : default
    {
        return (T)(object)((TimeOnly)request.value).ToTimeSpan();
    }
}