using Q11.Tools.Conversion.Handlers.Base;
using Q11.Tools.Conversion.Pocos;

namespace Q11.Tools.Conversion.Handlers;

internal class OtherHandler : HandlerConditional
{
    public override bool CanHandle<T>(ChangeTypeRequest<T> request)
    {
        return true;
    }

    public override T? GetValue<T>(ChangeTypeRequest<T> request) where T : default
    {
        return request.value != null ? (T) request.value : default;
    }
}