using System.Numerics;

namespace AresFramework.Networking;

public static class NetworkingConstants
{

    public static readonly int HttpPort;

    public static readonly int IdleTime = 15;

    public static readonly int JaggrabPort;
    
    public static readonly BigInteger RsaExponent;

    public static readonly BigInteger RsaModulus;
    

    static NetworkingConstants()
    {
        HttpPort = 2;
    }

}