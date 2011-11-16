using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpMQ
{
    class MessageKey
    {
        public ulong Id { get; set; }
        public uint Priority { get; set; }
        public uint Timeout { get; set; }
        public MessageStatus Status { get; set; }

        public MessageKey(ulong theId, uint thePriority, uint theTimeout, MessageStatus theStatus)
        {
            this.Id = theId;
            this.Priority = thePriority;
            this.Timeout = theTimeout;
            this.Status = theStatus;
        }
    }
}
