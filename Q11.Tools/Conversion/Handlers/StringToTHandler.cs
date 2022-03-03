namespace Q11.Tools.Conversion.Handlers;

internal class StringToTHandler<TTo> : HandlerBase
{
    public override bool CanHandle<T>(ChangeTypeRequest<T> request)
    {
        return request.valueIsString && request.toType == typeof(TTo);
    }

    public override T? GetValue<T>(ChangeTypeRequest<T> request) where T : default
    {
        return request.ValueCastedToT;
    }
}