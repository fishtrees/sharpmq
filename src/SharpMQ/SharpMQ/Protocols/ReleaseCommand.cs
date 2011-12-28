using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpMQ.Protocols
{
    internal class ReleaseCommand : Command
    {
        public long JobId { get; set; }
        public int Priority { get; set; }
        public int Delay { get; set; }

        public override byte[] ToBytes()
        {
            //release <id> <pri> <delay>\r\n
            StringBuilder cmd = new StringBuilder();
            cmd.Append("release");
            cmd.Append(ProtocolConstants.SEPARATOR);
            cmd.Append(this.JobId);
            cmd.Append(ProtocolConstants.SEPARATOR);
            cmd.Append(this.Priority);
            cmd.Append(ProtocolConstants.SEPARATOR);
            cmd.Append(this.Delay);
            cmd.Append(ProtocolConstants.TERMINATOR);
            return Encoding.ASCII.GetBytes(cmd.ToString());
        }
    }

    internal class ReleaseResponse : Reply
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
