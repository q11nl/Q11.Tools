using System.Globalization;
using System.Text.RegularExpressions;
using Q11.Tools.Conversion.Handlers.Base;
using Q11.Tools.Conversion.Pocos;
using Q11.Tools.Extensions;

namespace Q11.Tools.Conversion.Handlers;

internal class StringInScientificNotationToFloatingPointHandler : HandlerConditional
{
    public override bool CanHandle<T>(ChangeTypeRequest<T> request)
    {
        return request.valueIsString && IsTypeOfFloatingPoint<T>() &&
               IsValueInScientificNotation(request);
    }

    public override T? GetValue<T>(ChangeTypeRequest<T> request) where T : default
    {
        var convertedFloatingPointValue = ConvertScientificNotationToType(request);
        return request.GetValue(convertedFloatingPointValue);
    }

    private static bool IsTypeOfFloatingPoint<T>()
    {
        return OtherExtensions.IsIn(typeof(T), typeof(float), typeof(float?), typeof(double), typeof(double?), typeof(decimal),
            typeof(decimal?));
    }

    private static bool IsValueInScientificNotation<T>(ChangeTypeRequest<T> request)
    {

        return Regex.IsMatch(request.stringClean, "^[-+]?[0-9,.]+[E]{1}[-+]?[0-9]+$", RegexOptions.IgnoreCase);
    }

    private static object ConvertScientificNotationToType<T>(ChangeTypeRequest<T> request)
    {
        const NumberStyles numberStyle = NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent | NumberStyles.AllowLeadingSign;

        if (OtherExtensions.IsIn(typeof(T), typeof(double), typeof(double?)))
        {
            return double.Parse(request.stringClean, numberStyle, CultureInfo.InvariantCulture);
        }

        if (OtherExtensions.IsIn(typeof(T), typeof(float), typeof(float?)))
        {
            return float.Parse(request.stringClean, numberStyle, CultureInfo.InvariantCulture);
        }

        if (OtherExtensions.IsIn(typeof(T), typeof(decimal), typeof(decimal?)))
        {
            return decimal.Parse(request.stringClean, numberStyle, CultureInfo.InvariantCulture);
        }

        throw new InvalidOperationException($"{request.stringClean} can not be converted to type {typeof(T).FullName}.");
    }
}