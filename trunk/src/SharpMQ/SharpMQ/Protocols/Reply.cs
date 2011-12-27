using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpMQ.Protocols
{
    public abstract class Reply
    {
        public string Status { get; set; }

        public abstract byte[] CreateBytes();
    }
}
