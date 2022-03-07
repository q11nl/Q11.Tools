using System.Reflection;
using Q11.Tools.Conversion.Pocos;

namespace Q11.Tools.Conversion.Handlers.Base;

internal abstract class EnumerableToCollectionHandlerBase : HandlerBase
{
    public override ChangeTypeResponse<T> GetResponse<T>(ChangeTypeRequest<T> request)
    {
        var enumerableInterface = request.fromType.GetInterfaces()
            .FirstOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>));

        if (enumerableInterface == null || !CanHandToType(request.toType)) return CanNotHandle<T>();

        var enumerableFromType = enumerableInterface.UnderlyingSystemType.GenericTypeArguments.First();
        var result = CallCreateResultMethod(request, enumerableFromType);

        return GetHandledResult((T)result!);
    }

    private object? CallCreateResultMethod<T>(ChangeTypeRequest<T> request, Type enumerableFromType)
    {
        const string methodName = "CreateResult";
        var enumerableToType = GetElementType(request);

        var allMethods = GetType().GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public |
                                              BindingFlags.NonPublic);

        var getListMethod = allMethods.Single(m => m.Name == methodName);

        var genericGetListMethod = getListMethod.MakeGenericMethod(enumerableFromType, enumerableToType);
        var parameters = new[] { request.value };
        return genericGetListMethod.Invoke(this, parameters);
    }

    protected abstract Type GetElementType<T>(ChangeTypeRequest<T> request);

    protected abstract bool CanHandToType(Type toType);
}