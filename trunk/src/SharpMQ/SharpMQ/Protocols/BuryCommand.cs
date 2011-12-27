using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpMQ.Protocols
{
    internal class BuryCommand : Command
    {
        public long JobId { get; set; }
        public int Priority { get; set; }

        public override byte[] CreateBytes()
        {
            throw new NotImplementedException();
        }
    }

    internal class BuryResponse : Reply
    {

        public override byte[] CreateBytes()
        {
            throw new NotImplementedException();
        }
    }
}
