using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpMQ.Protocols
{
    internal class ReplyStatus
    {
        public static readonly string INSERTED = "INSERTED";
        public static readonly string BURIED = "BURIED";
        public static readonly string EXPECTED_CRLF = "EXPECTED_CRLF";
        public static readonly string JOB_TOO_BIG = "JOB_TOO_BIG";
        public static readonly string DRAINING = "DRAINING";
        public static readonly string USING = "USING";
        public static readonly string DEADLINE_SOON = "DEADLINE_SOON";
        public static readonly string TIMED_OUT = "TIMED_OUT";
        public static readonly string RESERVED = "RESERVED";
        public static readonly string DELETED = "DELETED";
        public static readonly string NOT_FOUND = "NOT_FOUND";
        public static readonly string RELEASED = "RELEASED";
        public static readonly string TOUCHED = "TOUCHED";
        public static readonly string FOUND = "FOUND";
        public static readonly string OK = "OK";
        public static readonly string NOT_IGNORED = "NOT_IGNORED";
        public static readonly string WATCHING = "WATCHING";
    }
}
