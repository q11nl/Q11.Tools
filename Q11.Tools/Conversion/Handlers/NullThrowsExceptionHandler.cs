using Q11.Tools.Conversion.Handlers.Base;
using Q11.Tools.Conversion.Pocos;

namespace Q11.Tools.Conversion.Handlers;

internal class NullThrowsExceptionHandler : HandlerBase
{
    public override ChangeTypeResponse<T> GetResponse<T>(ChangeTypeRequest<T> request)
    {
        if (request.value == null)
        {
            throw new ArgumentException("Null can not be converted to non nullable struct.", nameof(request.value));
        }

        return CanNotHandle<T>();
    }
}