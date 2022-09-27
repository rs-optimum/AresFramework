using AresFramework.Cache.Archives;
using AresFramework.Cache.Exceptions;
using AresFramework.Cache.Files;
using NLog;

namespace AresFramework.Cache;

public class GameCache : IDisposable
{

    private static readonly Logger Log = LogManager.GetCurrentClassLogger();

    public readonly Dictionary<FileDescriptor, Archive> CachedArchives = new Dictionary<FileDescriptor, Archive>(CacheConstants.ArchiveCount);

    /// <summary>
    /// If the cache is readonly
    /// </summary>
    private FileAccess CacheAccess { get; set; }
    
    private string CachePath { get; set; }

    private FileStream[] Indencies = new FileStream[256]; // Since there are only up to 256 files in the cache

    /// <summary>
    /// The data file
    /// </summary>
    private FileStream _data;
    
    
    
    public GameCache(string path, FileAccess cacheAccess)
    {
        CacheAccess = cacheAccess;
        CachePath = path;
    }

    public Archive GetArchive(int type, int file)
    {
        var fileDescriptor = new FileDescriptor(type, file);
        CachedArchives.TryGetValue(fileDescriptor, out var archive);

        if (archive == null)
        {
            archive = Archive.Decode(GetFile(fileDescriptor));
        }

        return archive;
    }

    public void LoadCache()
    {
        int amountOfIndexes = 0;

        for (int i = 0; i < Indencies.Length; i++)
        {
            var path = Path.Combine(CachePath, $"main_file_cache.idx{i}");
            bool indexExists = File.Exists(path);
            if (indexExists)
            {
                amountOfIndexes++;
                Indencies[i] = File.Open(path, FileMode.Open, CacheAccess);
            }
        }
        if (amountOfIndexes <= 0)
        {
            throw new InvalidCacheException("Cache location container 0 indexes");
        }
        
        var mainCacheFile = Path.Combine(CachePath, "main_file_cache.dat2");
        if (File.Exists(mainCacheFile))
        {
            _data = File.Open(mainCacheFile, FileMode.Open, CacheAccess);
            Log.Debug("Loaded main cache file!");
        }
        else
        {
            throw new InvalidCacheException("Could not locate main_file_cache.dat2");
        }
    }


    public BinaryReader GetFile(FileDescriptor descriptor)
    {
        CacheIndex index = GetIndex(descriptor);
        BinaryReader stream = new BinaryReader(new MemoryStream(index.Size));

        long position = index.Block * CacheConstants.BlockSize;
        int read = 0;
        int size = index.Size;
        
        return stream;
    }


    private CacheIndex GetIndex(FileDescriptor descriptor)
    {
        int index = descriptor.Type;
        byte[] buffer = new byte[CacheConstants.IndexSize];
        FileStream indexStream = Indencies[index];

        lock (indexStream)
        {
            long position = descriptor.File * CacheConstants.IndexSize;
            if (position >= 0 && indexStream.Length >= position + CacheConstants.IndexSize)
            {
                indexStream.Seek(position, SeekOrigin.Begin);
                var read = indexStream.Read(buffer, 0,  buffer.Length);
            }
        }
        
        return CacheIndex.Decode(buffer);
    }
    
    
    public void Dispose()
    {
        if (_data != null)
        {
            lock (_data)
            {
                _data.Dispose();
            }
        }


        foreach (var index in Indencies)
        {
            if (index != null)
            {
                lock (index)
                {
                    index.Dispose();
                }
            }
        }
        
    }
}