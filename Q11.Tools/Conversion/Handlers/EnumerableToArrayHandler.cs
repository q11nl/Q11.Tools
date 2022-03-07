using Q11.Tools.Conversion.Handlers.Base;
using Q11.Tools.Conversion.Pocos;

namespace Q11.Tools.Conversion.Handlers;

internal class EnumerableToArrayHandler : EnumerableToCollectionHandlerBase
{

    protected override Type GetElementType<T>(ChangeTypeRequest<T> request)
    {
        return request.toType.GetElementType()!;
    }

    protected override bool CanHandToType(Type toType)
    {
        return toType.IsArray;
    }

    // This method is called by reflection in the base class to create the result of the handler
    private Array CreateResult<TItemFrom, TItemTo>(object enumerableObject)
    {
        if (enumerableObject is IEnumerable<TItemFrom> enumerable)
        {
            return enumerable.Select(i => i.ChangeType<TItemTo>()).ToArray();
        }

        throw new ArgumentException("The fromType can not be converted to a list.", nameof(enumerableObject));
    }
}