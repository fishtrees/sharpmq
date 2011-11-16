using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSharpTest.Net.Serialization;

namespace SharpMQ
{
    class MessageKeySerializer : ISerializer<MessageKey>
    {
        public MessageKey ReadFrom(System.IO.Stream stream)
        {
            return new MessageKey(PrimitiveSerializer.UInt64.ReadFrom(stream)
                , PrimitiveSerializer.UInt32.ReadFrom(stream)
                , PrimitiveSerializer.UInt32.ReadFrom(stream)
                , (MessageStatus)PrimitiveSerializer.Int32.ReadFrom(stream));
        }

        public void WriteTo(MessageKey value, System.IO.Stream stream)
        {
            PrimitiveSerializer.UInt64.WriteTo(value.Id, stream);
            PrimitiveSerializer.UInt32.WriteTo(value.Priority, stream);
            PrimitiveSerializer.UInt32.WriteTo(value.Timeout, stream);
            PrimitiveSerializer.Int32.WriteTo((int)value.Status, stream);
        }
    }
}
