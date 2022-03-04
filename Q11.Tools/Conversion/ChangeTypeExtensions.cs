using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using Q11.Tools.Conversion.Handlers;
using Q11.Tools.Conversion.Handlers.Base;
using Q11.Tools.Conversion.Pocos;
using Q11.Tools.Extensions;

namespace Q11.Tools.Conversion
{
    public static class ChangeTypeExtensions
    {
        private static readonly CultureInfo DefaultCultureInfo = CultureInfo.InvariantCulture;
        private const bool DefaultReturnDefaultValueWhenPossible = true;
        private static readonly IReadOnlyList<IHandler> AllHandlers = GetAllHandlers();

        private static IReadOnlyList<IHandler> GetAllHandlers()
        {
            return new List<IHandler>
            {
                // This is not in random order!
                new NullToClassHandler(),
                new NonStringToStringHandler(),
                new StringToStringHandler(),
                new StringToEnumHandler(),
                new StringEmptyToNonStringHandler(),
                new StringToGuidHandler(),
                new StringInScientificNotationToFloatingPointHandler(),
                new StringIToTimeSpanHandler(),
                new StringToDateOnlyHandler(),
                new StringToTimeOnlyHandler(),
                new ToNullableStructHandler(),
                new NullThrowsExceptionHandler(),
                new ConvertibleToNonEnumConvertibleHandler(),
                new ToEnumHandler(),
                new DateTimeToDateOnlyHandler(),
                new DateTimeToTimeOnlyHandler(),
                new TimeOnlyToDateTimeHandler(),
                new DateOnlyToDateTimeHandler(),
                new TimeSpanToTimeOnlyHandler(),
                new TimeOnlyToTimeSpanHandler(),
                new OtherHandler(),
            };
        }

        /// <summary>
        /// Convert anything to T, possibly a null
        /// </summary>
        public static T? ChangeType<T>(this object? value, bool returnDefaultValueWhenPossible = DefaultReturnDefaultValueWhenPossible)
        {
            return ChangeType<T>(value, DefaultCultureInfo, returnDefaultValueWhenPossible);
        }

        internal static T? ChangeTypeImplementation<T>(this ChangeTypeRequest<T> request)
        {
            var response = AllHandlers.Select(h => h.GetResponse(request))
                                      .First(r => r.CanHandle);
            return response.ChangedValue;
        }

        /// <summary>
        /// Convert anything to T, possibly a null
        /// </summary>
        public static T? ChangeType<T>(this object? value, CultureInfo cultureInfo,
        bool returnDefaultValueWhenPossible = DefaultReturnDefaultValueWhenPossible)
        {
            var request = new ChangeTypeRequest<T>(value, cultureInfo, returnDefaultValueWhenPossible);
            //return ChangeTypeImplementation<T>(request);
            
            if (request.value == null && request.toType.IsClass) return default;
            
            var toType = typeof(T);

            if (value == null && toType.IsClass) return default;

            var stringValue = value as string;

            if (stringValue == null)
            {
                if (toType == typeof(string)) // It is not a string but it will be converted to a string
                {
                    return ChangeType<T>(Convert.ToString(value, cultureInfo), cultureInfo,
                        returnDefaultValueWhenPossible);
                }
            }
            else // It is a string
            {
                if (toType == typeof(string)) return (T)value; // It is a string and it will stay a string

                stringValue = stringValue.Trim();

                if (toType.IsEnum)
                {
                    return (T)Enum.Parse(typeof(T), stringValue);
                }

                if (stringValue.Length == 0 &&
                    toType != typeof(string)) // Empty string is a weird case, will convert to a default type
                {
                    if (returnDefaultValueWhenPossible && (toType.IsClass || IsNullableStruct(toType)))
                    {
                        return default;
                    }

                    throw new FormatException("Empty string is not in a correct format for a struct.");
                }

                if (toType == typeof(Guid)) // It is a string and it will be converted to a guid
                {
                    return
                        ChangeType<T>(new Guid(stringValue), cultureInfo,
                            returnDefaultValueWhenPossible); // Convert to guid can not be done with a default converter, constructor met with a parameter and default value are special cases
                }

                if (IsTypeOfFloatingPoint<T>() && IsValueInScientificNotation(stringValue))
                {
                    var convertedFloatingPointValue = ConvertScientificNotationToType<T>(stringValue);
                    return ChangeType<T>(convertedFloatingPointValue, cultureInfo, returnDefaultValueWhenPossible);
                }

                if (toType == typeof(TimeSpan) && TimeSpan.TryParse(stringValue, cultureInfo, out var timeSpan))
                {
                    return (T)(object)timeSpan;
                }

                if (toType == typeof(DateOnly) &&
                    DateOnly.TryParse(stringValue, cultureInfo, DateTimeStyles.None, out var dateOnly))
                {
                    return (T)(object)dateOnly;
                }

                if (toType == typeof(TimeOnly) &&
                    TimeOnly.TryParse(stringValue, cultureInfo, DateTimeStyles.None, out var timeOnly))
                {
                    return (T)(object)timeOnly;
                }
            }

            if (IsNullableStruct(toType))
            {
                toType = Nullable.GetUnderlyingType(toType)!;

                if (value == null)
                {
                    if (toType.IsEnum)
                    {
                        return default;
                    }

                    if (returnDefaultValueWhenPossible)
                    {
                        return default;
                    }

                    throw new InvalidCastException(
                        "Nullable struct type with null value can not be converted when value is null.");
                }

                if (toType.IsEnum)
                {
                    if (stringValue != null)
                    {
                        return (T)Enum.Parse(toType, stringValue);
                    }
                }
            }

            if (value == null)
            {
                throw new ArgumentException("Null can not be converted to non nullable struct.", nameof(value));
            }

            var fromType = value.GetType();

            // ReSharper disable once SuspiciousTypeConversion.Global, Generic Type T can be IConvertible
            var canConvert = value is IConvertible && ConvertsWithIConvertible(toType) && !toType.IsEnum;

            if (canConvert)
            {
                return (T)Convert.ChangeType(value, toType, cultureInfo);
            }

            if (toType.IsEnum)
            {
                // cast from nullable enum
                return (T)Enum.ToObject(toType, value);
            }

            if (fromType == typeof(DateTime))
            {
                if (toType == typeof(DateOnly))
                {
                    return (T)(object)DateOnly.FromDateTime((DateTime)value);
                }

                if (toType == typeof(TimeOnly))
                {
                    return (T)(object)TimeOnly.FromDateTime((DateTime)value);
                }
            }

            if (toType == typeof(DateTime))
            {
                if (fromType == typeof(TimeOnly))
                {
                    return (T)(object)(default(DateOnly).ToDateTime((TimeOnly)value));
                }

                if (fromType == typeof(DateOnly))
                {
                    return (T)(object)((DateOnly)value).ToDateTime(default);
                }
            }

            if (fromType == typeof(TimeSpan) && toType == typeof(TimeOnly))
            {
                var timeSpan = (TimeSpan)value;
                var timeSpanExcludingDays = timeSpan.Add(new TimeSpan(timeSpan.Days, 0, 0, 0).Negate());
                return (T)(object)new TimeOnly(timeSpanExcludingDays.Ticks);
            }

            if (fromType == typeof(TimeOnly) && toType == typeof(TimeSpan))
            {
                return (T)(object)((TimeOnly)value).ToTimeSpan();
            }

            return (T)value;
        }

        private static bool IsNullableStruct(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        private static bool IsValueInScientificNotation(object value)
        {
            if (value == null) return false;

            string eNotationString = (value.ToString() ?? string.Empty).Trim();

            return Regex.IsMatch(eNotationString, "^[-+]?[0-9,.]+[E]{1}[-+]?[0-9]+$", RegexOptions.IgnoreCase);
        }

        private static bool IsTypeOfFloatingPoint<T>()
        {
            return OtherExtensions.IsIn(typeof(T), typeof(float), typeof(float?), typeof(double), typeof(double?), typeof(decimal), typeof(decimal?));
        }

        private static object ConvertScientificNotationToType<T>(object value)
        {
            string eNotation = (value?.ToString() ?? string.Empty).Trim();

            var numberStyle = NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent | NumberStyles.AllowLeadingSign;

            if (OtherExtensions.IsIn(typeof(T), typeof(double), typeof(double?)))
            {
                return double.Parse(eNotation, numberStyle, CultureInfo.InvariantCulture);
            }

            if (OtherExtensions.IsIn(typeof(T), typeof(float), typeof(float?)))
            {
                return float.Parse(eNotation, numberStyle, CultureInfo.InvariantCulture);
            }

            if (OtherExtensions.IsIn(typeof(T), typeof(decimal), typeof(decimal?)))
            {
                return decimal.Parse(eNotation, numberStyle, CultureInfo.InvariantCulture);
            }

            throw new InvalidOperationException($"{value} can not be converted to type {typeof(T).FullName}.");
        }

        private static bool ConvertsWithIConvertible(Type type)
        {
            return OtherExtensions.IsIn(type, typeof(bool), typeof(char), typeof(sbyte), typeof(byte), typeof(short), typeof(ushort),
                             typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float), typeof(double),
                             typeof(decimal), typeof(DateTime), typeof(string));

        }

        public static object? ChangeType(this object? value, Type theTypeToChangeTo, CultureInfo cultureInfo, bool returnDefaultValueWhenPossible)
        {
            MethodInfo? method = typeof(ChangeTypeExtensions).GetMethod(nameof(ChangeTypeWithUniqueName), BindingFlags.NonPublic | BindingFlags.Static);
            method = method!.MakeGenericMethod(theTypeToChangeTo);
            var result = method.Invoke(null, new[] { value, returnDefaultValueWhenPossible, cultureInfo });

            return result;
        }

        /// <summary>
        /// Convert anything to T, possibly a null
        /// </summary>
        private static object? ChangeTypeWithUniqueName<T>(this object value, CultureInfo cultureInfo, bool returnDefaultValueWhenPossible)
        {
            return value.ChangeType<T>(cultureInfo, returnDefaultValueWhenPossible);
        }
    }
}
