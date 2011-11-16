using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpMQ
{
    class MessageData
    {
        public ulong Id { get; set; }
        public byte[] Data { get; set; }

        public MessageData(ulong theId, byte[] theData)
        {
            this.Id = theId;
            this.Data = theData;
        }
    }
}
