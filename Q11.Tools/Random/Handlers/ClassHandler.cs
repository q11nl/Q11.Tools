using System.Reflection;

namespace Q11.Tools.Random.Handlers;

internal class ClassHandler : HandlerBase
{
    public ClassHandler(GenericRandom genericRandom) : base(genericRandom)
    {
    }

    public override bool CanHandle<T>() => !typeof(T).IsValueType;

    public override T GetValue<T>(int level)
    {
        if (level > GenericRandom.MaximumLevelsDeep) return default!; // Prevent stack overflow, better leave it default then throw an exception
        level++;
        T instance = CreateObjectWithoutCallingConstructor<T>()!;
        var fieldInfos = instance.GetType().GetFields(BindingFlags.Public |
                                                      BindingFlags.NonPublic |
                                                      BindingFlags.Instance);
        foreach (var fieldInfo in fieldInfos)
        {
            var nextOrDefaultMethod = typeof(GenericRandom).GetMethod(nameof(GenericRandom.NextOrDefaultWithLevel),
                BindingFlags.Instance | BindingFlags.NonPublic)!;

            var genericNextOrDefaultMethod = nextOrDefaultMethod.MakeGenericMethod(fieldInfo.FieldType);
            var parameters = new object?[] {level};
            var valueForProperty = genericNextOrDefaultMethod.Invoke(GenericRandom, parameters);

            fieldInfo.SetValue(instance, valueForProperty);
        }
        return instance;
    }
}