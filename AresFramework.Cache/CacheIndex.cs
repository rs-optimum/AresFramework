namespace AresFramework.Cache;

public class CacheIndex
{
    public readonly int Block;

    public readonly int Size;

    public CacheIndex(int block, int size)
    {
        Block = block;
        Size = size;
    }
    
    public static CacheIndex Decode(byte[] buffer)
    {
        int size = (buffer[0] & 0xFF) << 16 | (buffer[1] & 0xFF) << 8 | buffer[2] & 0xFF;
        int block = (buffer[3] & 0xFF) << 16 | (buffer[4] & 0xFF) << 8 | buffer[5] & 0xFF;
        return new CacheIndex(block, size);
    }
}