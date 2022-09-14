using System.Text;

namespace AresFramework.Utilities;

public static class BufferUtils
{
    
    public static int ReadSmart(MemoryStream stream)
    {
        using var br = new BinaryReader(stream, Encoding.Default, true);
        var previousPosition = stream.Position;
        var peek = stream.ReadByte() & 0xFF;
        stream.Position = previousPosition;
        var value = peek > byte.MaxValue ? (br.ReadInt16() & 0xFFFF) + short.MinValue : br.ReadByte() & 0xFF;
        return value;
    }
    
    
    public static int ReadUnsignedMedium(MemoryStream stream)
    {
        using var br = new BinaryReader(stream, Encoding.Default, true);
        return (br.ReadInt16() & 0xFFFF) << 8 | br.ReadByte() & 0xFF;
    }
    
}