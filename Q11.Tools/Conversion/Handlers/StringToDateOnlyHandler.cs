using System.Globalization;
using Q11.Tools.Conversion.Handlers.Base;
using Q11.Tools.Conversion.Pocos;

namespace Q11.Tools.Conversion.Handlers;

internal class StringToDateOnlyHandler : HandlerBase
{
    public override ChangeTypeResponse<T> GetResponse<T>(ChangeTypeRequest<T> request)
    {
        if (request.valueIsString && request.IsToType<DateOnly>() &&
            DateOnly.TryParse(request.stringClean, request.cultureInfo, DateTimeStyles.None, out var result))
        {
            return GetHandledResult((T)(object)result);
        }

        return CanNotHandle<T>();
    }
}