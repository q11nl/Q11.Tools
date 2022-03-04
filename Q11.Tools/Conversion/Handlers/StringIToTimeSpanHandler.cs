using Q11.Tools.Conversion.Handlers.Base;
using Q11.Tools.Conversion.Pocos;

namespace Q11.Tools.Conversion.Handlers;

internal class StringIToTimeSpanHandler : HandlerBase
{
    public override ChangeTypeResponse<T> GetResponse<T>(ChangeTypeRequest<T> request)
    {
        if (request.valueIsString && request.IsToType<TimeSpan>() &&
            TimeSpan.TryParse(request.stringClean, request.cultureInfo, out var result))
        {
            return GetHandledResult((T)(object)result);
        }

        return CanNotHandle<T>();
    }
}