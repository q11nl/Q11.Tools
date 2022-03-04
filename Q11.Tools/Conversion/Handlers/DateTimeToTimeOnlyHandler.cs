using Q11.Tools.Conversion.Handlers.Base;
using Q11.Tools.Conversion.Pocos;

namespace Q11.Tools.Conversion.Handlers;

internal class DateTimeToTimeOnlyHandler : Handler<DateTime, TimeOnly>
{
    public override T? GetValue<T>(ChangeTypeRequest<T> request) where T : default
    {
        return (T)(object)TimeOnly.FromDateTime((DateTime)request.value);
    }
}