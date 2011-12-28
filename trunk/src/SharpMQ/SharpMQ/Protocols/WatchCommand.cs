using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpMQ.Protocols
{
    internal class WatchCommand : Command
    {
        public string TubeName { get; set; }

        public override byte[] ToBytes()
        {
            StringBuilder cmd = new StringBuilder();
            cmd.Append("watch");
            cmd.Append(ProtocolConstants.SEPARATOR);
            cmd.Append(this.TubeName);
            cmd.Append(ProtocolConstants.TERMINATOR);
            return Encoding.ASCII.GetBytes(cmd.ToString());
        }
    }

    internal class WatchReply : Reply
    {
        public int Count { get; set; }

        public override byte[] CreateBytes()
        {
            StringBuilder reply = new StringBuilder();
            reply.Append(this.Status);
            reply.Append(ProtocolConstants.SEPARATOR);
            reply.Append(this.Count);
            reply.Append(ProtocolConstants.TERMINATOR);
            return Encoding.ASCII.GetBytes(reply.ToString());
        }
    }
}
