using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FyndSharp.Communication.Common;

namespace SharpMQ.Protocols
{
    [Serializable]
    public abstract class Command : IMessage
    {
        public string Id { get; set; }

        public string RepliedId { get; set; }

        public abstract byte[] ToBytes();
    }
}
