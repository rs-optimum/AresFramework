using System.Text;

namespace AresFramework.Utility;

public static class BufferUtils
{
    
    public static int ReadSmart(this BinaryReader stream)
    {
        var previousPosition = stream.BaseStream.Position;
        var peek = stream.ReadByte() & 0xFF;
        stream.BaseStream.Position = previousPosition;
        var value = peek > byte.MaxValue ? (stream.ReadInt16() & 0xFFFF) + short.MinValue : stream.ReadByte() & 0xFF;
        return value;
    }
    
    public static int ReadUnsignedMedium(this BinaryReader stream)
    {
        return (stream.ReadInt16() & 0xFFFF) << 8 | stream.ReadByte() & 0xFF;
    }
    
}