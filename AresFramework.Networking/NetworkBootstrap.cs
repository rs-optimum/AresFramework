using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;

namespace AresFramework.Networking;

public class NetworkBootstrap : ServerBootstrap
{
    private IEventLoopGroup _parent;
    private IEventLoopGroup _childGroup;
    
}