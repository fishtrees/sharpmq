using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpMQ.Protocols
{
    internal static class ProtocolConstants
    {
        public static readonly string TERMINATOR = "\r\n";
        public static readonly char TERMINATOR_CHAR_1 = (char)10;
        public static readonly char TERMINATOR_CHAR_2 = (char)13;
        public static readonly char SEPARATOR = ' ';
    }
}
