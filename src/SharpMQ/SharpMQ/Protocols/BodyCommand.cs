using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpMQ.Protocols
{
    internal abstract class BodyCommand : Command
    {
        public byte[] Body { get; set; }
    }
}
