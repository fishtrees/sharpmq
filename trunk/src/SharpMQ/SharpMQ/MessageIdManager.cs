using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpMQ
{
    class MessageIdManager
    {
        public static readonly MessageIdManager Default = new MessageIdManager();

        private long _CurrentId = 0L;

        public long GetNextId()
        {
            return System.Threading.Interlocked.Increment(ref this._CurrentId);
        }
    }
}
