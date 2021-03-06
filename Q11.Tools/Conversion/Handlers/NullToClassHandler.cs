using Q11.Tools.Conversion.Handlers.Base;
using Q11.Tools.Conversion.Pocos;

namespace Q11.Tools.Conversion.Handlers;

internal class NullToClassHandler : HandlerConditional
{
    public override bool CanHandle<T>(ChangeTypeRequest<T> request)
    {
        return request.value == null && request.toType.IsClass;
    }

    public override T? GetValue<T>(ChangeTypeRequest<T> request) where T : default
    {
        return default;
    }
}