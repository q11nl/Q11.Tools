using Q11.Tools.Conversion.Handlers.Base;
using Q11.Tools.Conversion.Pocos;

namespace Q11.Tools.Conversion.Handlers;

internal class StringToGuidHandler : HandlerBase
{
    public override ChangeTypeResponse<T> GetResponse<T>(ChangeTypeRequest<T> request)
    {
        if (request.valueIsString && request.IsToType<Guid>() &&
            Guid.TryParse(request.stringClean, out var result))
        {
            return request.GetResponse((T)(object)result);
        }

        return CanNotHandle<T>();
    }
}