using Q11.Tools.Conversion.Handlers.Base;
using Q11.Tools.Conversion.Pocos;

namespace Q11.Tools.Conversion.Handlers;

internal class ToEnumHandler : HandlerConditional
{
    public override bool CanHandle<T>(ChangeTypeRequest<T> request)
    {
        return request.toType.IsEnum;
    }

    public override T? GetValue<T>(ChangeTypeRequest<T> request) where T : default
    {
        return (T)Enum.ToObject(request.toType, request.value!);
    }
}