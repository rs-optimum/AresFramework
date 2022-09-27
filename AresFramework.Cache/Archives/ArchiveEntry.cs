namespace AresFramework.Cache.Archives;

public class ArchiveEntry
{
    public readonly BinaryReader ByteBuffer;
    public readonly int Identifier;
    
    public ArchiveEntry(BinaryReader buffer, int identifier)
    {
        ByteBuffer = buffer;
        Identifier = identifier;
    }
}