using Q11.Tools.Conversion.Handlers.Base;
using Q11.Tools.Conversion.Pocos;

namespace Q11.Tools.Conversion.Handlers;

internal class DateTimeToDateOnlyHandler : Handler<DateTime, DateOnly>
{
    public override T? GetValue<T>(ChangeTypeRequest<T> request) where T : default
    {
        return (T)(object)DateOnly.FromDateTime((DateTime)request.value);
    }
}