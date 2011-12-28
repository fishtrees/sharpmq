using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpMQ.Protocols
{
    [Serializable]
    internal class PutCommand : BodyCommand
    {
        public int Priority { get; set; }
        public int Delay { get; set; }
        public int TimeToRun { get; set; }


        public override byte[] ToBytes()
        {
//put <pri> <delay> <ttr> <bytes>\r\n
//<data>\r\n
            StringBuilder cmd = new StringBuilder();
            cmd.Append("put");
            cmd.Append(ProtocolConstants.SEPARATOR);
            cmd.Append(this.Priority);
            cmd.Append(ProtocolConstants.SEPARATOR);
            cmd.Append(this.Delay);
            cmd.Append(ProtocolConstants.SEPARATOR);
            cmd.Append(this.TimeToRun);
            cmd.Append(ProtocolConstants.SEPARATOR);
            cmd.Append(this.Body.Length);
            cmd.Append(ProtocolConstants.TERMINATOR);
            byte[] theHeadBytes = Encoding.ASCII.GetBytes(cmd.ToString());
            byte[] theBytes = new byte[theHeadBytes.Length + this.Body.Length + 2];
            Array.Copy(theHeadBytes, 0, theBytes, 0, theHeadBytes.Length);
            theBytes[theBytes.Length - 2] = (byte)ProtocolConstants.TERMINATOR_CHAR_1;
            theBytes[theBytes.Length - 1] = (byte)ProtocolConstants.TERMINATOR_CHAR_1;
            return theBytes;
        }
    }

    internal class PutResponse : Reply
    {
        public long JobId { get; set; }

        public override byte[] CreateBytes()
        {
            StringBuilder reply = new StringBuilder();
            reply.Append(this.Status);
            if (this.Status == ReplyStatus.INSERTED || this.Status == ReplyStatus.BURIED)
            {
                reply.Append(ProtocolConstants.SEPARATOR);
                reply.Append(this.JobId);
            }
            reply.Append(ProtocolConstants.TERMINATOR);
            return Encoding.ASCII.GetBytes(reply.ToString());
        }
    }
}
