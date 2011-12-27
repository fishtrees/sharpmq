using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpMQ.Protocols
{
    internal class DeleteCommand : Command
    {
        public long JobId { get; set; }

        public override byte[] CreateBytes()
        {
            StringBuilder cmd = new StringBuilder();
            cmd.Append("delete");
            cmd.Append(ProtocolConstants.SEPARATOR);
            cmd.Append(this.JobId);
            cmd.Append(ProtocolConstants.TERMINATOR);
            return Encoding.ASCII.GetBytes(cmd.ToString());
        }
    }

    public class DeleteReply : Reply
    {
        public override byte[] CreateBytes()
        {
            StringBuilder reply = new StringBuilder();
            reply.Append(this.Status);
            reply.Append(ProtocolConstants.TERMINATOR);
            return Encoding.ASCII.GetBytes(reply.ToString());
        }
    }
}
