using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpMQ.Protocols
{
    internal class ReserveCommand : Command
    {
        public override byte[] ToBytes()
        {
            StringBuilder cmd = new StringBuilder();
            cmd.Append("reserve");
            cmd.Append(ProtocolConstants.TERMINATOR);
            return Encoding.ASCII.GetBytes(cmd.ToString());
        }
    }

    internal class ReserveWithTimeoutCommand : Command
    {
        public int Timeout { get; set; }

        public override byte[] ToBytes()
        {
            StringBuilder cmd = new StringBuilder();
            cmd.Append("reserve-with-timeout");
            cmd.Append(ProtocolConstants.SEPARATOR);
            cmd.Append(this.Timeout);
            cmd.Append(ProtocolConstants.TERMINATOR);
            return Encoding.ASCII.GetBytes(cmd.ToString());
        }
    }

    internal class ReserveReply : BodyReply
    {
        public long JobId { get; set; }

        public override byte[] CreateBytes()
        {
            StringBuilder reply = new StringBuilder();
            reply.Append(this.Status);
            if (this.Status == ReplyStatus.RESERVED)
            {
                reply.Append(ProtocolConstants.SEPARATOR);
                reply.Append(this.JobId);
                reply.Append(ProtocolConstants.SEPARATOR);
                reply.Append(this.Body.Length);
                reply.Append(ProtocolConstants.TERMINATOR);
                byte[] theHeadBytes = Encoding.ASCII.GetBytes(reply.ToString());
                byte[] theBytes = new byte[theHeadBytes.Length + this.Body.Length + 2];
                Array.Copy(theHeadBytes, 0, theBytes, 0, theHeadBytes.Length);
                theBytes[theBytes.Length - 2] = (byte)ProtocolConstants.TERMINATOR_CHAR_1;
                theBytes[theBytes.Length - 1] = (byte)ProtocolConstants.TERMINATOR_CHAR_1;
                return theBytes;
            }
            reply.Append(ProtocolConstants.TERMINATOR);
            return Encoding.ASCII.GetBytes(reply.ToString());
        }
    }
}
