using Q11.Tools.Conversion.Handlers.Base;
using Q11.Tools.Conversion.Pocos;
using Q11.Tools.Extensions;

namespace Q11.Tools.Conversion.Handlers;

internal class EnumerableToListHandler : EnumerableToCollectionHandlerBase
{
    protected override Type GetElementType<T>(ChangeTypeRequest<T> request)
    {
        return request.toType.UnderlyingSystemType.GenericTypeArguments.First();
}

    protected override bool CanHandToType(Type toType)
    {
        return toType.IsGenericType && toType.GetGenericTypeDefinition() == typeof(List<>)
               || toType.IsGenericType && toType.GetGenericTypeDefinition()
                   .IsIn(typeof(IEnumerable<>), typeof(IList<>), typeof(IReadOnlyList<>));
    }

    // This method is called by reflection in the base class to create the result of the handler
    private List<TItemTo?> CreateResult<TItemFrom, TItemTo>(object enumerableObject)
    {
        if (enumerableObject is IEnumerable<TItemFrom> enumerable)
        {
            return enumerable.Select(i => i.ChangeType<TItemTo>()).ToList();
        }

        throw new ArgumentException("The fromType can not be converted to a list.", nameof(enumerableObject));
    }
}