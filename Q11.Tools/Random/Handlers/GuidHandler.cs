namespace Q11.Tools.Random.Handlers;

internal class GuidHandler : HandlerBase
{
    public GuidHandler(GenericRandom genericRandom) : base(genericRandom)
    {
    }

    public override bool CanHandle<T>() => IsSameType<T, Guid>();

    public override T GetValue<T>(int level)
    {
        const int guidLength = 16;

        var bytes = Enumerable.Range(1, guidLength).Select(_ => GenericRandom.NextWithLevel<byte>(level)).ToArray();
        SetGuidToVersion4();
        return new Guid(bytes).ConvertTo<Guid, T>();
        ;

        void SetGuidToVersion4()
        {
            const int indexOfVersionNumber = 7;
            const int guid4Version = 0x40; 
            bytes[indexOfVersionNumber] = (byte)(bytes[indexOfVersionNumber] & 0x0f | guid4Version);
        }
    }
}