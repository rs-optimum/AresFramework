using AresFramework.Utilities;

namespace AresFramework.Cache.Archives;

public class Archive
{
    public readonly ArchiveEntry[] ArchiveEntries;
    
    public Archive(ArchiveEntry[] archiveEntries)
    {
        ArchiveEntries = archiveEntries;
        
    }

    public ArchiveEntry GetEntry(string name)
    {
        int hash = Hash(name);

        foreach (var archive in ArchiveEntries)
        {
            if (archive.Identifier == hash)
            {
                return archive;
            }
        }

        throw new Exception("Could not find entry " + name);
    }
    
    public static Archive Decode(MemoryStream buffer)
    {
        int extractSize = BufferUtils.ReadUnsignedMedium(buffer);
        int size = BufferUtils.ReadUnsignedMedium(buffer);
        
        return null;
    }
    
    public static int Hash(string name)
    {
        var charArray = name.ToUpper().ToCharArray();
        
        return Enumerable.Range(0, charArray.Length).Sum((a) =>
        {
            int current = charArray[a].GetHashCode();
            return a * 61 + current - 32;
        });
    }
    
}