using System.Net.Sockets;
using DotNetty.Transport.Channels;
using Coldairarrow.DotNettySocket;
namespace AresFramework.Networking;

public class HttpChannel : ChannelInitializer<IChannel>
{
    
    private readonly IChannelHandlerContext _handler;

    public HttpChannel(IChannelHandlerContext handler)
    {
        _handler = handler;
    }
    
    protected override void InitChannel(IChannel channel)
    {
        channel.Pipeline.AddLast("decoder", null);
    }
}