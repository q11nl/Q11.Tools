using Q11.Tools.Conversion.Pocos;

namespace Q11.Tools.Conversion.Handlers.Base;

internal interface IHandler
{
    ChangeTypeResponse<T> GetResponse<T>(ChangeTypeRequest<T> request);
}