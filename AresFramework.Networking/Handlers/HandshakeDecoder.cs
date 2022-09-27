using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;

namespace AresFramework.Networking.Handlers;

public class HandshakeDecoder : ByteToMessageDecoder
{
    protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
    {
        var hello = input.MarkReaderIndex();
    }
}