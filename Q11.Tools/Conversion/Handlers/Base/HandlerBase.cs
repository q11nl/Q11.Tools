using Q11.Tools.Conversion.Pocos;

namespace Q11.Tools.Conversion.Handlers.Base;

internal abstract class HandlerBase : IHandler
{
    public abstract ChangeTypeResponse<T> GetResponse<T>(ChangeTypeRequest<T> request);

    public ChangeTypeResponse<T> GetHandledResult<T>(T? value)
    {
        return ChangeTypeResponse<T>.Create(value);
    }

    public ChangeTypeResponse<T> CanNotHandle<T>()
    {
        return ChangeTypeResponse<T>.Create();
    }
}