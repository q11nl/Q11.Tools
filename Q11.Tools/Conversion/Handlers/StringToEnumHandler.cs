﻿namespace Q11.Tools.Conversion.Handlers;

internal class StringToEnumHandler : HandlerBase
{
    public override bool CanHandle<T>(ChangeTypeRequest<T> request)
    {
        return request.valueIsString && request.toType.IsEnum;
    }

    public override T? GetValue<T>(ChangeTypeRequest<T> request) where T : default
    {
        return (T)Enum.Parse(typeof(T), request.StringClean);
    }
}