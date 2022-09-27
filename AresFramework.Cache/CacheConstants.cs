namespace AresFramework.Cache;

public static class CacheConstants
{
    
    public const int ArchiveCount = 16;

    public const int ChunkSize = 512;

    public const int HeaderSize = 8;

    public const int BlockSize = HeaderSize + ChunkSize;

    public const int IndexSize = 6;

}