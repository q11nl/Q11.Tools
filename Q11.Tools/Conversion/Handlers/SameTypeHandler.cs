using Q11.Tools.Conversion.Handlers.Base;
using Q11.Tools.Conversion.Pocos;

namespace Q11.Tools.Conversion.Handlers;

internal class SameTypeHandler : HandlerConditional
{
    public override bool CanHandle<T>(ChangeTypeRequest<T> request)
    {
        return request.IsFromToType<T, T>();
    }

    public override T? GetValue<T>(ChangeTypeRequest<T> request) where T : default
    {
        return request.value != null ? (T)request.value : default;
    }
}