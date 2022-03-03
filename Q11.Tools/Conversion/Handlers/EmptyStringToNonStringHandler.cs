namespace Q11.Tools.Conversion.Handlers;

internal class EmptyStringToNonStringHandler : HandlerBase
{
    public override bool CanHandle<T>(ChangeTypeRequest<T> request)
    {
        return request.StringClean == "" && !request.IsToType<string>();
    }

    public override T? GetValue<T>(ChangeTypeRequest<T> request) where T : default
    {
        if (request.returnDefaultValueWhenPossible && (request.toType.IsClass || request.ToTypeIsNullableStruct))
        {
            return default;
        }

        throw new FormatException("Empty string is not in a correct format for a struct.");
    }
}