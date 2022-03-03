namespace Q11.Tools.Conversion.Handlers;

internal interface IHandler
{
    bool CanHandle<T>(ChangeTypeRequest<T> request);
    T? GetValue<T>(ChangeTypeRequest<T> request);
}

internal static class ChangeTypeRequestExtensions
{
    public static T? ChangeType<T>(this ChangeTypeRequest<T> request, object? newValue)
    {
        return newValue.ChangeType<T>(request.cultureInfo, request.returnDefaultValueWhenPossible);
    }
}

internal abstract class HandlerBase : IHandler
{
    public abstract bool CanHandle<T>(ChangeTypeRequest<T> request);
    public abstract T? GetValue<T>(ChangeTypeRequest<T> request);
}

internal class Handler<TFrom, TTo> : IHandler
{
    public bool CanHandle<T>(ChangeTypeRequest<T> request)
    {
        return request.IsFromType<TFrom>() && request.IsToType<TTo>();
    }

    public virtual T? GetValue<T>(ChangeTypeRequest<T> request)
    {
        return request.ValueCastedToT;
    }
}