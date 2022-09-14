namespace AresFramework.Cache.Archives;

public class ArchiveEntry
{
    public readonly MemoryStream ByteBuffer;

    public readonly int Identifier;

    public ArchiveEntry(MemoryStream buffer, int identifier)
    {
        ByteBuffer = buffer;
        Identifier = identifier;
    }
}