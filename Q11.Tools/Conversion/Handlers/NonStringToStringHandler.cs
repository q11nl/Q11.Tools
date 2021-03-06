using Q11.Tools.Conversion.Handlers.Base;
using Q11.Tools.Conversion.Pocos;

namespace Q11.Tools.Conversion.Handlers;

internal class NonStringToStringHandler : HandlerConditional
{
    public override bool CanHandle<T>(ChangeTypeRequest<T> request)
    {
        return !request.valueIsString && request.IsToType<string>();
    }

    public override T? GetValue<T>(ChangeTypeRequest<T> request) where T : default
    {
        return request.GetValue(Convert.ToString(request.value, request.cultureInfo));
    }
}
