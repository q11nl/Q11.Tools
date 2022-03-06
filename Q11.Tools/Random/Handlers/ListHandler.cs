using System.Reflection;

namespace Q11.Tools.Random.Handlers;

internal class ListHandler : HandlerBase
{
    public ListHandler(GenericRandom genericRandom) : base(genericRandom)
    {
    }

    public override bool CanHandle<T>()
    {
        var type = typeof(T);
        return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
    }

    public override T GetValue<T>(int level)
    {
        var type = typeof(T);
        var typeOfListItem = type.GetGenericArguments()[0];

        var getListMethod = typeof(ListHandler).GetMethod(nameof(GetList),
            BindingFlags.Instance | BindingFlags.NonPublic)!;

        var genericGetListMethod = getListMethod.MakeGenericMethod(typeOfListItem);
        var parameters = new object?[]{level};
        var valueForProperty = genericGetListMethod.Invoke(this, parameters);
        return (T) valueForProperty!;
    }

    private List<T> GetList<T>(int level)
    {
        const int maximumItemCount = 5; 
        if (level > GenericRandom.MaximumLevelsDeep) return new List<T>(); // Prevent stack overflow
        level++;
        return Enumerable
            .Range(1, GetTnt(1, maximumItemCount))
            .Select(_ => GenericRandom.NextWithLevel<T>(level)).ToList();
    }
}