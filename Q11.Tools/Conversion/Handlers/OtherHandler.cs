namespace Q11.Tools.Conversion.Handlers;

internal class OtherHandler : HandlerBase
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