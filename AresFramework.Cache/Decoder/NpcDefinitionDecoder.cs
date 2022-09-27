using NLog;

namespace AresFramework.Cache.Decoder;

public class NpcDefinitionDecoder : IDecoder
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();
    
    private readonly GameCache _cache;

    public NpcDefinitionDecoder(GameCache cache)
    {
        _cache = cache;
    }


    public void Decode()
    {
        try
        {
            var archive = _cache.GetArchive(0, 2);
            var data = archive.GetEntry("obj.dat").ByteBuffer;
            var idx = archive.GetEntry("obj.idx").ByteBuffer;

            int count = idx.ReadInt16();
            int index = 2;

            int[] indices = new int[count];
        }
        catch(Exception ex)
        {
            Log.Error(ex, "cant decode");
        }
    }
}