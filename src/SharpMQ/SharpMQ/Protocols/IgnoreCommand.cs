using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpMQ.Protocols
{
    internal class IgnoreCommand : Command
    {
        public string TubeName { get; set; }

        public override byte[] ToBytes()
        {
            StringBuilder cmd = new StringBuilder();
            cmd.Append("ignore");
            cmd.Append(ProtocolConstants.SEPARATOR);
            cmd.Append(this.TubeName);
            cmd.Append(ProtocolConstants.TERMINATOR);
            return Encoding.ASCII.GetBytes(cmd.ToString());
        }
    }

    internal class IgnoreReply : Reply
    {
        public int TubeCount { get; set; }

        public override byte[] CreateBytes()
        {
            StringBuilder reply = new StringBuilder();
            reply.Append(this.Status);
            if (this.Status == ReplyStatus.WATCHING)
            {
                reply.Append(ProtocolConstants.SEPARATOR);
                reply.Append(this.TubeCount);
            }
            reply.Append(ProtocolConstants.TERMINATOR);
            return Encoding.ASCII.GetBytes(reply.ToString());
        }
    }
}
