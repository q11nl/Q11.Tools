namespace Q11.Tools.Conversion.Handlers;

internal class StringToGuidHandler : StringToTHandler<Guid>
{
    public override T? GetValue<T>(ChangeTypeRequest<T> request) where T : default
    {
        if (Guid.TryParse(request.StringClean, out var result))
        {
            // Convert to guid can not be done with a default converter, constructor met with a parameter and default value are special cases
            return request.ChangeType(result);
        }

        throw new FormatException("Empty string is not in a correct format for a struct.");
    }
}