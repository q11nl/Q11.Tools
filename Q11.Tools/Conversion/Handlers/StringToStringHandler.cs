using Q11.Tools.Conversion.Handlers.Base;
using Q11.Tools.Conversion.Pocos;

namespace Q11.Tools.Conversion.Handlers;

internal class StringToStringHandler : Handler<string, string>
{
    public override T? GetValue<T>(ChangeTypeRequest<T> request) where T : default
    {
        return (T)(object)request.value;
    }
}