namespace Q11.Tools.Conversion.Pocos;

internal struct ChangeTypeResponse<T>
{
    public static ChangeTypeResponse<T> Create(T? changedValue)
    {
        return new ChangeTypeResponse<T>(true, changedValue);
    }

    public static ChangeTypeResponse<T> Create()
    {
        return new ChangeTypeResponse<T>(false, default);
    }

    public bool CanHandle { get; }
    public T? ChangedValue { get; }

    private ChangeTypeResponse(bool canHandle, T changedValue)
    {
        CanHandle = canHandle;
        ChangedValue = changedValue;
    }
}