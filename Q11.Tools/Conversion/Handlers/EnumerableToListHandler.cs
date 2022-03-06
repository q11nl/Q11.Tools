using System.Reflection;
using Q11.Tools.Conversion.Handlers.Base;
using Q11.Tools.Conversion.Pocos;
using Q11.Tools.Extensions;

namespace Q11.Tools.Conversion.Handlers;

internal class EnumerableToListHandler : HandlerBase
{
    public override ChangeTypeResponse<T> GetResponse<T>(ChangeTypeRequest<T> request)
    {
        var enumerableInterface = request.fromType.GetInterfaces()
            .FirstOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>));
        if (enumerableInterface == null) return CanNotHandle<T>();

        if (!CanHandToType(request.toType)) return CanNotHandle<T>();

        var enumerableFromType = enumerableInterface.UnderlyingSystemType.GenericTypeArguments.First();
        var result = CallGenericListMethod(request, enumerableFromType);

        return GetHandledResult((T)result!);
    }

    private object? CallGenericListMethod<T>(ChangeTypeRequest<T> request, Type enumerableFromType)
    {
        var enumerableToType = request.toType.UnderlyingSystemType.GenericTypeArguments.First();
        var getListMethod = GetType().GetMethod(nameof(CreateResult),
            BindingFlags.Instance | BindingFlags.NonPublic)!;
        var genericGetListMethod = getListMethod.MakeGenericMethod(enumerableFromType, enumerableToType);
        var parameters = new[] {request.value};
        return genericGetListMethod.Invoke(this, parameters);
    }

    private bool CanHandToType(Type toType)
    {
        return toType.IsGenericType && toType.GetGenericTypeDefinition() == typeof(List<>)
               || toType.IsGenericType && toType.GetGenericTypeDefinition()
                   .IsIn(typeof(IEnumerable<>), typeof(IList<>), typeof(IReadOnlyList<>));
    }

    private List<TItemTo?> CreateResult<TItemFrom, TItemTo>(object enumerableObject)
    {
        if (enumerableObject is IEnumerable<TItemFrom> enumerable)
        {
            return enumerable.Select(i => i.ChangeType<TItemTo>()).ToList();
        }

        throw new ArgumentException("The fromType can not be converted to a list.", nameof(enumerableObject));
    }
}

internal class EnumerableToArrayHandler : HandlerBase
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
        var enumerableToType = GetElementType(request);
        var getListMethod = GetType().GetMethod(nameof(CreateResult),
            BindingFlags.Instance | BindingFlags.NonPublic)!;
        var genericGetListMethod = getListMethod.MakeGenericMethod(enumerableFromType, enumerableToType);
        var parameters = new[] { request.value };
        return genericGetListMethod.Invoke(this, parameters);
    }

    protected Type GetElementType<T>(ChangeTypeRequest<T> request)
    {
        return request.toType.GetElementType()!;
    }

    private bool CanHandToType(Type toType)
    {
        return toType.IsArray;
    }

    private Array CreateResult<TItemFrom, TItemTo>(object enumerableObject)
    {
        if (enumerableObject is IEnumerable<TItemFrom> enumerable)
        {
            return enumerable.Select(i => i.ChangeType<TItemTo>()).ToArray();
        }

        throw new ArgumentException("The fromType can not be converted to a list.", nameof(enumerableObject));
    }
}