using Q11.Tools.Conversion.Pocos;

namespace Q11.Tools.Conversion.Handlers.Base;

internal abstract class HandlerConditional : HandlerBase
{
    public override ChangeTypeResponse<T> GetResponse<T>(ChangeTypeRequest<T> request)
    {
        if (CanHandle(request))
        {
            var result = GetValue(request);
            return GetHandledResult(result);
        }

        return CanNotHandle<T>();
    }

    public abstract bool CanHandle<T>(ChangeTypeRequest<T> request);

    public abstract T? GetValue<T>(ChangeTypeRequest<T> request);
}