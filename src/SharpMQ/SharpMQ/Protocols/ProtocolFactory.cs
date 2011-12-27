using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using FyndSharp.Communication.Protocols;

namespace SharpMQ.Protocols
{
    public class ProtocolFactory : IProtocolFactory
    {
        public IProtocol CreateProtocol()
        {
            return new HeadBodyProtocol();
        }
    }
}
