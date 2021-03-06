using Q11.Tools.Conversion.Handlers.Base;
using Q11.Tools.Conversion.Pocos;

namespace Q11.Tools.Conversion.Handlers;

internal class DateOnlyToDateTimeHandler : Handler<DateOnly, DateTime>
{
    public override T? GetValue<T>(ChangeTypeRequest<T> request) where T : default
    {
        return (T)(object)((DateOnly)request.value).ToDateTime(default);
    }
}