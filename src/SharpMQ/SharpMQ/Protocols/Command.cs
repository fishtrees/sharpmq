using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpMQ.Protocols
{
    [Serializable]
    public abstract class Command
    {
        public abstract byte[] CreateBytes();
    }
}
