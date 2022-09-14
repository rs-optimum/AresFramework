namespace AresFramework.Cache.Files;

public class FileDescriptor
{
    public readonly int File;
    public readonly int Type;

    public FileDescriptor(int file, int type)
    {
        File = file;
        Type = type;
    }

    public override bool Equals(object? obj)
    {
        if (obj is FileDescriptor)
        {
            FileDescriptor other = (FileDescriptor) obj;
            return Type == other.Type && File == other.File;
        }
        return false;
    }
    
    public override int GetHashCode()
    {
        return File * CacheConstants.ArchiveCount + Type;
    }
}